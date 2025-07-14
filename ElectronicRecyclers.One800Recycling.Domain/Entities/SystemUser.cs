using System;
using System.Text;
using System.Security.Cryptography;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ElectronicRecyclers.One800Recycling.Domain.Common;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using ElectronicRecyclers.One800Recycling.Domain.Exceptions;

namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    [Serializable]
    public class SystemUser : DomainObject
    {
        private const string CONSTANT_PASSWORD_SALT  = "Ycq37Eri073v3#";

        public virtual Name Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string HashedPassword { get; protected set; }

        public virtual DateTimeOffset LastLoginOn { get; set; }

        private string passwordSalt;
        public virtual string PasswordSalt 
        { 
            get 
            {
                return passwordSalt ?? (passwordSalt = Guid.NewGuid().ToString("N"));
            } 
            set 
            {
                passwordSalt = value;
            }
        }

        public virtual bool IsEnabled { get; set; }

        private string GetHashedPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                var computedHash = sha.ComputeHash(Encoding
                    .Unicode
                    .GetBytes(PasswordSalt + password + CONSTANT_PASSWORD_SALT));

                return Convert.ToBase64String(computedHash);
            }
        }

        public virtual void SetPassword(string password)
        {
            HashedPassword = GetHashedPassword(password);
        }

        public virtual bool ValidatePassword(string maybePassword)
        {
            if (HashedPassword == null)
                return true;

            var isValidated = HashedPassword == GetHashedPassword(maybePassword);

            if (isValidated)
                LastLoginOn = DateTimeOffset.Now;

            return isValidated;
        }

        protected SystemUser() { }

        public SystemUser(string firstName, string lastName, string email, string password)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrWhiteSpace(email), "Email is required.");
            Guard.Against<ArgumentNullException>(string.IsNullOrWhiteSpace(password), "Password is required.");

            Name = new Name(firstName, lastName);
            Email = email;
            LastLoginOn = DateTime.Now;
            IsEnabled = true;
            SetPassword(password);
        }

        private readonly ICollection<Role> roles = new Collection<Role>();

        public virtual void AddRole(Role role)
        {
            if (roles.Contains(role))
                return;

            roles.Add(role);
        }

        public virtual bool RemoveRole(Role role)
        {
            if (roles.Contains(role) == false)
                return false;

            Guard.Against<BusinessException>(roles.Count == 1, "User should be associated at least with one role.");

            return roles.Remove(role);
        }

        public virtual Role GetRole(int id)
        {
            return roles.FirstOrDefault(c => c.Id == id);
        }

        public virtual bool RemoveRole(int id)
        {
            return RemoveRole(GetRole(id));
        }

        public virtual bool IsInRole(string roleName)
        {
            return roles.Any(r => r.Name == roleName);
        }

        public virtual bool IsInRole(Role role)
        {
            return IsInRole(role.Name);
        }

        public virtual IEnumerable<Role> GetRoles()
        {
            return roles;
        }
    }
}