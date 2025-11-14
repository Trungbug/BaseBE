using Misa.demo.core.Interface.Repository;
using MySqlConnector;
using Microsoft.Extensions.Configuration;
using Dapper;
using Misa.demo.core.Exceptions;
using System.Reflection; 
using Misa.demo.core.Attibute; 
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
                var tableName = GetTableName(); // Nâng cấp
                var sql = $"SELECT * FROM {tableName}";
                var data = connection.Query<T>(sql);
                return data;
            }
        }

        public T Get(Guid id)
        {
            using (var connection = GetOpenConnection())
            {
                var tableName = GetTableName(); 
                var idColumn = GetPrimaryKeyColumnName(); 
                var sql = $"SELECT * FROM {tableName} WHERE {idColumn} = @Id";
                var data = connection.QueryFirstOrDefault<T>(sql, new { Id = id });
                return data;
            }
        }

        public int Insert(T entity)
        {
            
            CheckUnique(entity); 

            
            try
            {
                using (var connection = GetOpenConnection())
                {
                    var properties = typeof(T).GetProperties();
                    var tableName = GetTableName();

                    var columns = new List<string>();
                    var values = new List<string>();
                    var parameters = new DynamicParameters();

                    foreach (var prop in properties)
                    {
                        var isPrimaryKey = prop.GetCustomAttribute<PrimaryKeyAttribute>() != null;

                        
                        if (isPrimaryKey && prop.PropertyType == typeof(Guid))
                        {
                            prop.SetValue(entity, Guid.NewGuid());
                        }

                        var columnName = GetColumnName(prop);
                        columns.Add(columnName);
                        values.Add($"@{prop.Name}"); 
                        parameters.Add($"@{prop.Name}", prop.GetValue(entity));
                    }

                    var sql = $@"INSERT INTO {tableName} ({string.Join(", ", columns)}) 
                                 VALUES ({string.Join(", ", values)})";
                    var res = connection.Execute(sql, param: parameters);
                    return res;
                }
            }
            
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) 
                {
                   
                    throw new ValidationException("Mã đã tồn tại trong hệ thống.");
                }
                throw; 
            }
        }

        public int Update(T entity, Guid id)
        {
            
            CheckUnique(entity, id);

            
            using (var connection = GetOpenConnection())
            {
                var properties = typeof(T).GetProperties();
                var tableName = GetTableName();
                var idColumn = GetPrimaryKeyColumnName();

                var setClause = new List<string>();
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                foreach (var prop in properties)
                {
                    var isPrimaryKey = prop.GetCustomAttribute<PrimaryKeyAttribute>() != null;
                    if (isPrimaryKey) continue; 

                    var columnName = GetColumnName(prop);
                    setClause.Add($"{columnName} = @{prop.Name}");
                    parameters.Add($"@{prop.Name}", prop.GetValue(entity));
                }

                var sql = $@"UPDATE {tableName} SET {string.Join(", ", setClause)} 
                             WHERE {idColumn} = @Id";
                var res = connection.Execute(sql, param: parameters);
                return res;
            }
        }

        public int Delete(Guid id)
        {
            using (var connection = GetOpenConnection())
            {
                var tableName = GetTableName();
                var idColumn = GetPrimaryKeyColumnName(); 

                var sql = $@"DELETE FROM {tableName} WHERE {idColumn} = @Id";
                var res = connection.Execute(sql, new { Id = id });
                return res;
            }
        }

        #region Helper functions (Giống project tham khảo)

        /// <summary>
        /// Lấy tên bảng (từ Attribute [Table])
        /// </summary>
        private string GetTableName()
        {
            var tableAttr = typeof(T).GetCustomAttribute<TableAttribute>();
            if (tableAttr == null)
            {
                
                throw new Exception($"Entity {typeof(T).Name} thiếu Attribute [Table(\"...\")]");
                
            }
            return tableAttr.TableName;
        }

        /// <summary>
        /// Lấy tên cột (từ Attribute [ColumnName])
        /// </summary>
        private string GetColumnName(PropertyInfo prop)
        {
            var columnAttr = prop.GetCustomAttribute<ColumnNameAttribute>();
            if (columnAttr == null)
            {
                
                throw new Exception($"Property {prop.Name} của Entity {typeof(T).Name} thiếu Attribute [ColumnName(\"...\")]");
               
            }
            return columnAttr.Name;
        }

        /// <summary>
        /// Lấy tên cột khóa chính (từ Attribute [PrimaryKey])
        /// </summary>
        private string GetPrimaryKeyColumnName()
        {
            var pkProp = typeof(T).GetProperties()
                           .FirstOrDefault(p => p.GetCustomAttribute<PrimaryKeyAttribute>() != null);

            if (pkProp == null)
            {
                throw new Exception($"Entity {typeof(T).Name} thiếu Attribute [PrimaryKey]");
            }
            
            return GetColumnName(pkProp);
        }

        /// <summary>
        /// (MỚI) Kiểm tra các trường [Unique]
        /// (Giống CheckCodeExist của project tham khảo)
        /// </summary>
        private void CheckUnique(T entity, Guid? id = null)
        {
            var tableName = GetTableName();

           
            var uniqueProps = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<UniqueAttribute>() != null);

            if (!uniqueProps.Any()) return;

            using (var connection = GetOpenConnection())
            {
                foreach (var prop in uniqueProps)
                {
                    var uniqueAttr = prop.GetCustomAttribute<UniqueAttribute>();
                    var columnName = GetColumnName(prop);
                    var propValue = prop.GetValue(entity);

                    
                    if (propValue == null || string.IsNullOrEmpty(propValue.ToString()))
                    {
                        continue;
                    }

                    var sqlWhere = $"{columnName} = @PropValue";
                    var parameters = new DynamicParameters();
                    parameters.Add("@PropValue", propValue);

                   
                    if (id != null)
                    {
                        var idColumn = GetPrimaryKeyColumnName();
                        sqlWhere += $" AND {idColumn} <> @Id";
                        parameters.Add("@Id", id);
                    }

                    var sql = $"SELECT COUNT(1) FROM {tableName} WHERE {sqlWhere}";
                    var count = connection.ExecuteScalar<int>(sql, parameters);

                    if (count > 0)
                    {
                      
                        throw new ValidationException(uniqueAttr.ErrorMessage);
                    }
                }
            }
        }

        #endregion
    }
}