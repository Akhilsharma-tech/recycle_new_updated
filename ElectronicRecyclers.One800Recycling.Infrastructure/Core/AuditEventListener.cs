using ElectronicRecyclers.One800Recycling.Domain.Entities;
using NHibernate.Collection;
using NHibernate.Event;
using System;
using System.Text;

namespace ElectronicRecyclers.One800Recycling.Infrastructure.Core
{
    public class AuditEventListener : IPreInsertEventListener, IPreUpdateEventListener, IPreDeleteEventListener
    {
        private void InsertAuditEntry(
            AbstractPreDatabaseOperationEvent @event, 
            StringBuilder stringBuilder,
            AuditEventType eventType)
        {
            var userName = Thread.CurrentPrincipal.Identity.IsAuthenticated
                    ? Thread.CurrentPrincipal.Identity.Name
                    : "unknown";

            using (var session = NHSessionProvider.OpenSession())
            {
                session.Save(new AuditEntry
                {
                    Changes = stringBuilder.ToString(),
                    EntityId = (int)@event.Id,
                    EntityName = @event.Persister.EntityName,
                    Action = eventType,
                    ChangedOn = DateTime.Now,
                    ChangedBy = userName,
                });

                session.Flush();
            }
        }

        public bool OnPreInsert(PreInsertEvent @event)
        {
            if (@event.Entity is AuditEntry)
                return false;

            var stringBuilder = new StringBuilder();
            for (var i = 0; i < @event.State.Length; i++)
            {
                stringBuilder
                    .Append(@event.Persister.PropertyNames[i])
                    .Append(": ")
                    .Append(@event.State[i])
                    .AppendLine();
            }

            InsertAuditEntry(@event, stringBuilder, AuditEventType.Insert);
            return false;
        }

        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            Guard.Against<InvalidOperationException>(@event.Entity is AuditEntry, "You can't update audit entry.");

            var entityPersister = @event.Persister;

            var stringBuilder = new StringBuilder();
            for (var i = 0; i < @event.State.Length; i++)
            {
                if (@event.State == null || 
                    @event.State[i] == null ||
                    @event.OldState == null ||
                    @event.OldState[i] == null ||
                    @event.Persister.PropertyTypes[i]
                        .IsEqual(@event.OldState[i], @event.State[i]))
                    continue;

                stringBuilder
                    .Append(entityPersister.PropertyNames[i])
                    .Append(": ")
                    .Append(@event.OldState[i])
                    .Append(" -> ")
                    .Append(@event.State[i])
                    .AppendLine();
            }

            InsertAuditEntry(@event, stringBuilder, AuditEventType.Update);
            return false;
        }

        public bool OnPreDelete(PreDeleteEvent @event)
        {
            Guard.Against<InvalidOperationException>(@event.Entity is AuditEntry, "You can't delete audit entry.");

            var stringBuilder = new StringBuilder();
            for (var i = 0; i < @event.DeletedState.Length; i++)
            {
                if (@event.DeletedState[i] is AbstractPersistentCollection)
                    continue;

                stringBuilder
                    .Append(@event.Persister.PropertyNames[i])
                    .Append(": ")
                    .Append(@event.DeletedState[i])
                    .AppendLine();
            }

            InsertAuditEntry(@event, stringBuilder, AuditEventType.Delete);
            return false;
        }

        public async Task<bool> OnPreInsertAsync(PreInsertEvent @event, CancellationToken cancellationToken)
        {
            return await Task.FromResult(OnPreInsert(@event));
        }

        public async Task<bool> OnPreUpdateAsync(PreUpdateEvent @event, CancellationToken cancellationToken)
        {
            return await Task.FromResult(OnPreUpdate(@event));
        }

        public async Task<bool> OnPreDeleteAsync(PreDeleteEvent @event, CancellationToken cancellationToken)
        {
            return await Task.FromResult(OnPreDelete(@event));
        }
    }
}