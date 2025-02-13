using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using StatsLoader.API.Response.Wildberries.DeserializableStruct;

namespace StatsLoader.Data
{
    public class DatabaseService
    {
        public async Task InitializeDatabase()
        {
            using (var connection = new NpgsqlConnection(AppConfig.ConnectionString))
            {
                await connection.OpenAsync();

                // ✅ Проверяем соединение с БД
                Console.WriteLine("Connected to DB");

                // ✅ Проверяем, есть ли таблицы
                string checkTablesQuery = @"
                    SELECT table_name FROM information_schema.tables 
                    WHERE table_schema = 'public';";

                var tables = await connection.QueryAsync<string>(checkTablesQuery);
                Console.WriteLine($"Existing tables: {string.Join(", ", tables)}");

                if (!tables.Contains("reportdetailbyperiod"))
                {
                    Console.WriteLine("Table 'reportdetailbyperiod' does not exist. Creating...");
                    string createTableQuery = GenerateCreateTableQuery<ResponseReportDetailByPeriod>("reportdetailbyperiod");
                    await connection.ExecuteAsync(createTableQuery);
                }
                else
                {
                    Console.WriteLine("Table 'reportdetailbyperiod' already exists.");
                }
            }
        }

        public async Task SaveDataAsync<T>(string tableName, List<T> data)
        {
            if (data == null || data.Count == 0)
            {
                Console.WriteLine($"No data to save for {tableName}");
                return;
            }

            using (var connection = new NpgsqlConnection(AppConfig.ConnectionString))
            {
                await connection.OpenAsync();
                await EnsureTableExists<T>(connection, tableName);
                await EnsureColumnsExist<T>(connection, tableName);

                Console.WriteLine($"Saving {data.Count} records to {tableName}...");

                string insertQuery = GenerateInsertQuery<T>(tableName);
                await connection.ExecuteAsync(insertQuery, data);

                Console.WriteLine($"Data saved successfully!");
            }
        }


        private async Task EnsureTableExists<T>(NpgsqlConnection connection, string tableName)
        {
            string checkTableQuery = @"
                SELECT EXISTS (
                    SELECT 1 FROM information_schema.tables 
                    WHERE table_schema = 'public' 
                    AND table_name = @TableName
                );";

            bool tableExists = await connection.ExecuteScalarAsync<bool>(checkTableQuery, new { TableName = tableName });

            if (!tableExists)
            {
                Console.WriteLine($"Table '{tableName}' does not exist. Creating...");
                string createTableQuery = GenerateCreateTableQuery<T>(tableName);
                await connection.ExecuteAsync(createTableQuery);
            }
        }

        private string GenerateCreateTableQuery<T>(string tableName)
        {
            var properties = typeof(T).GetProperties();
            var columns = properties
                .Select(p => $"{p.Name.ToLower()} {GetPostgresType(p.PropertyType)}")
                .ToList();

            return $@"
                CREATE TABLE IF NOT EXISTS {tableName} (
                    id SERIAL PRIMARY KEY,
                    {string.Join(", ", columns)}
                );";
        }

        private async Task EnsureColumnsExist<T>(NpgsqlConnection connection, string tableName)
        {
            var existingColumns = await GetExistingColumns(connection, tableName);
            var newColumns = typeof(T).GetProperties()
                .Where(p => !existingColumns.Contains(p.Name.ToLower()))
                .Select(p => $"{p.Name.ToLower()} {GetPostgresType(p.PropertyType)}")
                .ToList();

            if (newColumns.Count > 0)
            {
                foreach (var column in newColumns)
                {
                    string alterQuery = $"ALTER TABLE {tableName} ADD COLUMN {column};";
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
            string query = @"
                SELECT column_name 
                FROM information_schema.columns 
                WHERE table_name = @TableName;";

            return (await connection.QueryAsync<string>(query, new { TableName = tableName })).ToList();
        }

        private string GetPostgresType(Type type)
        {
            return type switch
            {
                Type t when t == typeof(int) || t == typeof(long) => "BIGINT",
                Type t when t == typeof(double) || t == typeof(float) || t == typeof(decimal) => "DECIMAL",
                Type t when t == typeof(bool) => "BOOLEAN",
                Type t when t == typeof(DateTime) => "TIMESTAMP",
                _ => "TEXT"
            };
        }
    }
}
