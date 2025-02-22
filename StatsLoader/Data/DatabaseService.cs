using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using Dapper;
using Npgsql;
using System.Threading.Tasks;

namespace StatsLoader.Data
{
    public class DatabaseService
    {

        public async Task SaveDataAsync<T>(string tableName, List<T> data) 
        {
            if (data == null || data.Count == 0)
            {
                Console.WriteLine($"No data to save for {tableName}");
                return;
            }

            try
            {
                await using NpgsqlConnection connection = new NpgsqlConnection(AppConfig.ConnectionString);
                await connection.OpenAsync();

                string createTableQuery = GenerateCreateTableQuery<T>(tableName);
                await connection.ExecuteAsync(createTableQuery, commandTimeout: 1000);

                Console.WriteLine($"Saving {data.Count} records to {tableName}...");

                string insertQuery = GenerateInsertQuery<T>(tableName);

                await using var transaction = connection.BeginTransaction();
                try
                {
                    await connection.ExecuteAsync(insertQuery, data, transaction, commandTimeout: 1000);
                    await transaction.CommitAsync();
                    Console.WriteLine($"Data saved successfully to {tableName}");
                }
                catch (Exception txEx)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Transaction Failed: {txEx.Message}");
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data to {tableName}: {ex.Message}");
            }
        }

        private string GetColumnName(PropertyInfo p)
        {
            var attr = p.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                        .Cast<JsonPropertyNameAttribute>()
                        .FirstOrDefault();
            return attr?.Name ?? p.Name.ToLower();
        }

        private string GenerateCreateTableQuery<T>(string tableName)
        {
            var properties = typeof(T).GetProperties();
            var columns = properties.Select(p => $"\"{GetColumnName(p)}\" {GetPostgresType(p.PropertyType)}").ToList();
            string columnsPart = columns.Count > 0 ? string.Join(", ", columns) : "";
            return $"CREATE TABLE IF NOT EXISTS \"{tableName}\" ({columnsPart});";
        }

        private string GenerateInsertQuery<T>(string tableName)
        {
            var properties = typeof(T).GetProperties();
            var columnNames = string.Join(", ", properties.Select(p => $"\"{GetColumnName(p)}\""));
            var paramNames = string.Join(", ", properties.Select(p => $"@{p.Name}"));
            return $"INSERT INTO \"{tableName}\" ({columnNames}) VALUES ({paramNames});";
        }


        private string GetPostgresType(Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type) ?? type;
            if (underlyingType == typeof(int) || underlyingType == typeof(long))
                return "BIGINT";
            if (underlyingType == typeof(double) || underlyingType == typeof(float) || underlyingType == typeof(decimal))
                return "DECIMAL";
            if (underlyingType == typeof(bool))
                return "BOOLEAN";
            if (underlyingType == typeof(DateTime))
                return "TIMESTAMP";
            return "TEXT";
        }
    }
}
