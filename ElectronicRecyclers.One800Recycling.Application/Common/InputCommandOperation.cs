using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.Common
{
    public abstract class InputCommandOperation

    {

        private readonly string _connectionString;

        protected readonly List<SqlParameter> Parameters = new();

        protected InputCommandOperation(string connectionString)

        {

            _connectionString = connectionString;

        }

        public async Task<List<DynamicReader>> ExecuteAsync(CancellationToken cancellationToken = default)

        {

            var results = new List<DynamicReader>();

            await using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync(cancellationToken);

            await using var command = connection.CreateCommand();

            PrepareCommand(command);

            command.Parameters.AddRange(Parameters.ToArray());

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);

            while (await reader.ReadAsync(cancellationToken))

            {

                results.Add(CreateRowFromReader(reader));

            }

            return results;

        }

        protected abstract DynamicReader CreateRowFromReader(IDataReader reader);

        protected abstract void PrepareCommand(IDbCommand cmd);

        protected void AddParameter(string name, object value)

        {

            Parameters.Add(new SqlParameter($"@{name}", value ?? DBNull.Value));

        }

    }

}
