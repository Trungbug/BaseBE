using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.demo.core.Interface.Service;

namespace Misa_FS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public  class MSBaseController<T> : ControllerBase 
    {
        IBaseService<T> _baseService;
        
        [HttpPost]
        public IActionResult Post(T entity)
        {
            var connectionString = "server=localhost;uid=root;pwd=123456789;database=demo_net";
            var connection = new MySqlConnector.MySqlConnection(connectionString);
            var properties = typeof(T).GetProperties();
            var tableName = typeof(T).Name.ToLower();

            var columns = "";
            var values = "";
            var parameters = new DynamicParameters();

            foreach (var prop in properties)
            {
                // Chuyển property sang snake_case
                var columnName = ToSnakeCase(prop.Name);
                columns += $"{columnName},";
                values += $"@{prop.Name},";
                parameters.Add($"@{prop.Name}", prop.GetValue(entity));
            }

            columns = columns.TrimEnd(',');
            values = values.TrimEnd(',');
            var sql = $@"INSERT INTO {tableName} ({columns}) VALUES ({values})";
            var res = connection.Execute(sql, param: parameters);
            return Ok(res);
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    //var connectionString = "server=localhost;uid=root;pwd=123456789;database=demo_net";
        //    //var connection = new MySqlConnector.MySqlConnection(connectionString);
        //    //var tableName = typeof(T).Name.ToLower();

        //    //var sql = $"SELECT * FROM {tableName}";
        //    //var data = connection.Query<T>(sql);
        //    //return Ok(data);
        //}

        [HttpPut]
        public IActionResult Put(T entity)
        {
            var connectionString = "server=localhost;uid=root;pwd=123456789;database=demo_net";
            var connection = new MySqlConnector.MySqlConnection(connectionString);
            var properties = typeof(T).GetProperties();
            var tableName = typeof(T).Name.ToLower();

            var setClause = "";
            var parameters = new DynamicParameters();
            string idColumn = "";
            string idPropName = "";
            object idValue = null;

            foreach (var prop in properties)
            {
                var columnName = ToSnakeCase(prop.Name);

                if (prop.Name.ToLower().EndsWith("id"))
                {
                    idColumn = columnName;
                    idPropName = prop.Name;
                    idValue = prop.GetValue(entity);
                    parameters.Add($"@{prop.Name}", idValue);
                }
                else
                {
                    setClause += $"{columnName} = @{prop.Name},";
                    parameters.Add($"@{prop.Name}", prop.GetValue(entity));
                }
            }

            setClause = setClause.TrimEnd(',');
            var sql = $@"UPDATE {tableName} SET {setClause} WHERE {idColumn} = @{idPropName}";
            var res = connection.Execute(sql, param: parameters);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var connectionString = "server=localhost;uid=root;pwd=123456789;database=demo_net";
            var connection = new MySqlConnector.MySqlConnection(connectionString);
            var tableName = typeof(T).Name.ToLower();

            var idProp = typeof(T).GetProperties().FirstOrDefault(p => p.Name.ToLower().EndsWith("id"));
            var idColumn = ToSnakeCase(idProp.Name);

            var sql = $@"DELETE FROM {tableName} WHERE {idColumn} = @Id";
            var res = connection.Execute(sql, new { Id = id });
            return Ok(res);
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
