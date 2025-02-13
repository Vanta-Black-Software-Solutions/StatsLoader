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
        /// <summary>
        /// Инициализирует базу данных и создаёт таблицы, если они отсутствуют.
        /// </summary>
        public async Task InitializeDatabase()
        {
            try
            {
                await using var connection = new NpgsqlConnection(AppConfig.ConnectionString);
                await connection.OpenAsync();

                Console.WriteLine("Connected to DB");

                string createTableQuery = GenerateCreateTableQuery<ResponseReportDetailByPeriod>("reportdetailbyperiod");
                await connection.ExecuteAsync(createTableQuery, commandTimeout: 1000);

                Console.WriteLine("Database initialized.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Initialization Failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Сохраняет данные в БД.
        /// </summary>
        public async Task SaveDataAsync<T>(string tableName, List<T> data)
        {
            if (data == null || data.Count == 0)
            {
                Console.WriteLine($"No data to save for {tableName}");
                return;
            }

            try
            {
                await using var connection = new NpgsqlConnection(AppConfig.ConnectionString);
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

        /// <summary>
        /// Создаёт SQL-запрос для создания таблицы.
        /// </summary>
        private string GenerateCreateTableQuery<T>(string tableName)
        {
            var properties = typeof(T).GetProperties();
            var columns = properties.Select(p => $"{p.Name.ToLower()} {GetPostgresType(p.PropertyType)}");

            return $@"
                CREATE TABLE IF NOT EXISTS {tableName} (
                    id SERIAL PRIMARY KEY,
                    {string.Join(", ", columns)}
                );";
        }

        /// <summary>
        /// Генерирует SQL-запрос для вставки данных.
        /// </summary>
        private string GenerateInsertQuery<T>(string tableName)
        {
            var properties = typeof(T).GetProperties();
            var columnNames = string.Join(", ", properties.Select(p => p.Name.ToLower()));
            var paramNames = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            return $@"
                INSERT INTO {tableName} ({columnNames})
                VALUES ({paramNames});";
        }

        /// <summary>
        /// Определяет PostgreSQL тип по C# типу.
        /// </summary>
        private string GetPostgresType(Type type) => type switch
        {
            Type t when t == typeof(int) || t == typeof(long) => "BIGINT",
            Type t when t == typeof(double) || t == typeof(float) || t == typeof(decimal) => "DECIMAL",
            Type t when t == typeof(bool) => "BOOLEAN",
            Type t when t == typeof(DateTime) => "TIMESTAMP",
            _ => "TEXT"
        };
    }
}
