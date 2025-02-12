using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace StatsLoader.Data
{
    public class DatabaseService : IDatabaseService
    {
        public async Task SaveDataAsync<T>(string tableName, List<T> data)
        {
            if (data == null || data.Count == 0) return;

            using (NpgsqlConnection connection = new NpgsqlConnection(AppConfig.connectionString))
            {
                await connection.OpenAsync();

                string createTableQuery = GenerateCreateTableQuery<T>(tableName);
                await connection.ExecuteAsync(createTableQuery);

                await EnsureColumnsExist<T>(connection, tableName);

                string insertQuery = GenerateInsertQuery<T>(tableName);
                await connection.ExecuteAsync(insertQuery, data);
            }
        }



        private string GenerateCreateTableQuery<T>(string tableName)
        {
            var properties = typeof(T).GetProperties();
            var columns = properties.Select(p => $"{p.Name.ToLower()} {GetPostrgeType(p.PropertyType)}");

            return $@"CREATE TABLE IF NOT EXIST {tableName} (id SERIAL PRIMARY KEY, {string.Join(", ", columns)});";
        }


        private async Task EnsureColumnsExist<T>(NpgsqlConnection connection, string tableName)
        {
            var existingColumns = await GetExistingColumns(connection, tableName);
            var properties = typeof(T).GetProperties()
                .Select(p => p.Name.ToLower())
                .Where(p => !existingColumns.Contains(p))
                .ToList();

            if (properties.Count > 0)
            {
                foreach (var property in properties)
                {
                    string alterQuery = $@"ALTER TABLE {tableName} ADD COLUMN {property} TEXT";
                    await connection.ExecuteAsync(alterQuery);
                }
            }
        }

        private string GenerateInsertQuery<T>(string tableName)
        {
            var properties = typeof(T).GetProperties();
            var columnNames = string.Join(", ", properties.Select(p => p.Name.ToLower()));
            var paramNames = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            return $@"
                INSERT INTO {tableName} ({columnNames})
                VALUES ({paramNames});";
        }



        private async Task<List<string>> GetExistingColumns(NpgsqlConnection connection, string tableName)
        {
            string query = $@"SELECT column_name FROM information_schema.columns WHERE table_name = @TableName;";

            return (await connection.QueryAsync<string>(query, new {TableName = tableName})).AsList();
        }

        private string GetPostrgeType(Type type)
        {
            if (type == typeof(int) || type == typeof(long)) return "BIGINT";
            if (type == typeof(double) || type == typeof(float) || type == typeof(decimal)) return "DECIMAL";
            if (type == typeof(bool)) return "BOOLEAN";
            if (type == typeof(DateTime)) return "TIMESTAMP";
            return "TEXT";
        }
    }
}
