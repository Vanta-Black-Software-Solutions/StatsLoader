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
                    await connection.ExecuteAsync(insertQuery, data, transaction, commandTimeout: 10000);
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
        /// Версия метода сохранения, которая перед вставкой удаляет из списка отчёты,
        /// уже присутствующие в БД. Для этого определяется последний сохранённый отчёт,
        /// а затем в новом списке удаляются все элементы до (и включая) него.
        /// Предполагается, что и БД, и передаваемый список отсортированы по порядку вставки.
        /// </summary>
        public async Task SaveDataAsyncV2<T>(string tableName, List<T> data)
        {
            List<T> filteredData = await RemoveExistingReports<T>(tableName, data);
            if (filteredData == null || filteredData.Count == 0)
            {
                Console.WriteLine($"No new data to save for {tableName}");
                return;
            }

            await SaveDataAsync(tableName, filteredData);
        }

        /// <summary>
        /// Извлекает из БД последний сохранённый отчёт и ищет его в переданном списке.
        /// Все элементы, предшествующие найденному, считаются уже сохранёнными.
        /// Если последний отчёт не найден – предполагается, что пересечения нет.
        /// </summary>
        private async Task<List<T>> RemoveExistingReports<T>(string tableName, List<T> data)
        {
            if (data == null || data.Count == 0)
                return data;

            await using var connection = new NpgsqlConnection(AppConfig.ConnectionString);
            await connection.OpenAsync();

            T lastDbRecord = default;
            // Если у типа T есть свойство "id" (int или long), используем его для сортировки
            PropertyInfo idProperty = typeof(T)
                .GetProperties()
                .FirstOrDefault(p => string.Equals(p.Name, "id", StringComparison.OrdinalIgnoreCase)
                                  && (p.PropertyType == typeof(int) || p.PropertyType == typeof(long)));

            if (idProperty != null)
            {
                string sql = $"SELECT * FROM \"{tableName}\" ORDER BY \"{GetColumnName(idProperty)}\" DESC LIMIT 1";
                lastDbRecord = await connection.QueryFirstOrDefaultAsync<T>(sql);
            }
            else
            {
                // Если свойства "id" нет – выбираем все записи и берем последний.
                // При условии, что таблица содержит не очень много данных (например, за последний день).
                string sql = $"SELECT * FROM \"{tableName}\"";
                var records = (await connection.QueryAsync<T>(sql)).ToList();
                if (records.Any())
                {
                    lastDbRecord = records.Last();
                }
            }

            if (lastDbRecord == null)
            {
                // Таблица пуста – сохраняем все данные.
                return data;
            }

            // Ищем в новом списке индекс последнего элемента, уже сохранённого в БД.
            int index = data.FindLastIndex(x => AreEqual(x, lastDbRecord));
            if (index >= 0)
            {
                // Все отчёты до (и включая) найденный считаются уже сохранёнными.
                return data.Skip(index + 1).ToList();
            }
            else
            {
                // Если не найден – предполагаем, что пересечения нет.
                return data;
            }
        }

        /// <summary>
        /// Сравнивает два объекта типа T по всем публичным свойствам, за исключением свойства "id".
        /// Используется для определения идентичности отчётов.
        /// </summary>
        private bool AreEqual<T>(T obj1, T obj2)
        {
            if (obj1 == null && obj2 == null)
                return true;
            if (obj1 == null || obj2 == null)
                return false;

            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                if (string.Equals(prop.Name, "id", StringComparison.OrdinalIgnoreCase))
                    continue;

                var value1 = prop.GetValue(obj1);
                var value2 = prop.GetValue(obj2);

                if (value1 == null && value2 == null)
                    continue;
                if (value1 == null || value2 == null)
                    return false;
                if (!value1.Equals(value2))
                    return false;
            }
            return true;
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
            var columns = new List<string>();
            foreach (var p in properties)
            {
                if (string.Equals(p.Name, "id", StringComparison.OrdinalIgnoreCase))
                {
                    // Используем BIGSERIAL для автоинкремента и назначаем первичным ключом
                    columns.Add($"\"{GetColumnName(p)}\" BIGSERIAL PRIMARY KEY");
                }
                else
                {
                    columns.Add($"\"{GetColumnName(p)}\" {GetPostgresType(p.PropertyType)}");
                }
            }
            string columnsPart = columns.Count > 0 ? string.Join(", ", columns) : "";
            return $"CREATE TABLE IF NOT EXISTS \"{tableName}\" ({columnsPart});";
        }

        private string GenerateInsertQuery<T>(string tableName)
        {
            // Исключаем свойство "id" из запроса на вставку
            var properties = typeof(T).GetProperties()
                                      .Where(p => !string.Equals(p.Name, "id", StringComparison.OrdinalIgnoreCase));
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
