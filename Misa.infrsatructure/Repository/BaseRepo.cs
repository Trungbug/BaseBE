using Misa.demo.core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Microsoft.Extensions.Configuration;
using Dapper;
using Misa.demo.core.Exceptions; // Đảm bảo đã thêm Dapper

namespace Misa.infrsatructure.Repository
{
    public class BaseRepo<T> : IBaseRepo<T>
    {

        protected readonly string _connectionString;

        public BaseRepo(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ConnectionString");
        }


        protected MySqlConnection GetOpenConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public IEnumerable<T> GetAll()
        {
            using (var connection = GetOpenConnection())
            {
                var tableName = ToSnakeCase(typeof(T).Name);
                var sql = $"SELECT * FROM {tableName}";
                var data = connection.Query<T>(sql);
                return data;
            }
        }

        public T Get(Guid id)
        {
            using (var connection = GetOpenConnection())
            {
                var tableName = ToSnakeCase(typeof(T).Name);
                var idColumn = GetIdColumnName();
                var sql = $"SELECT * FROM {tableName} WHERE {idColumn} = @Id";
                var data = connection.QueryFirstOrDefault<T>(sql, new { Id = id });
                return data;
            }
        }

        public int Insert(T entity)
        {
            // Bọc code của bạn trong try-catch
            try
            {
                using (var connection = GetOpenConnection())
                {
                    var properties = typeof(T).GetProperties();
                    var tableName = ToSnakeCase(typeof(T).Name);

                    var columns = "";
                    var values = "";
                    var parameters = new DynamicParameters();

                    foreach (var prop in properties)
                    {
                        var columnName = ToSnakeCase(prop.Name);
                        columns += $"{columnName},";
                        values += $"@{prop.Name},";
                        parameters.Add($"@{prop.Name}", prop.GetValue(entity));
                    }

                    columns = columns.TrimEnd(',');
                    values = values.TrimEnd(',');
                    var sql = $@"INSERT INTO {tableName} ({columns}) VALUES ({values})";
                    var res = connection.Execute(sql, param: parameters);
                    return res;
                }
            }
            // Bắt lỗi cụ thể của MySQL
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) // 1062 = Duplicate Entry
                {
                    // Ném ra lỗi nghiệp vụ mà Service hiểu được
                    throw new ValidationException("Mã đã tồn tại trong hệ thống.");
                }
                throw; // Ném các lỗi MySQL khác
            }
        }

        public int Update(T entity, Guid id)
        {
            using (var connection = GetOpenConnection())
            {
                var properties = typeof(T).GetProperties();
                var tableName = ToSnakeCase(typeof(T).Name);

                var setClause = "";
                var parameters = new DynamicParameters();
                var idColumn = GetIdColumnName();
                parameters.Add("@Id", id);

                foreach (var prop in properties)
                {
                    var columnName = ToSnakeCase(prop.Name);

                    if (string.Equals(columnName, idColumn, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    setClause += $"{columnName} = @{prop.Name},";
                    parameters.Add($"@{prop.Name}", prop.GetValue(entity));
                }

                setClause = setClause.TrimEnd(',');
                var sql = $@"UPDATE {tableName} SET {setClause} WHERE {idColumn} = @Id";
                var res = connection.Execute(sql, param: parameters);
                return res;
            }
        }

        public int Delete(Guid id)
        {
            using (var connection = GetOpenConnection())
            {
                var tableName = ToSnakeCase(typeof(T).Name);
                var idColumn = GetIdColumnName();

                var sql = $@"DELETE FROM {tableName} WHERE {idColumn} = @Id";
                var res = connection.Execute(sql, new { Id = id });
                return res;
            }
        }

        private string GetIdColumnName()
        {
            var idProp = typeof(T).GetProperties().FirstOrDefault(p => p.Name.ToLower().EndsWith("id"));
            if (idProp == null)
            {

                return "id";
            }
            return ToSnakeCase(idProp.Name);
        }


        private string ToSnakeCase(string name)
        {
            return string.Concat(
                name.Select((c, i) => i > 0 && char.IsUpper(c)
                    ? "_" + char.ToLower(c)
                    : char.ToLower(c).ToString())
            );
        }
    }
}