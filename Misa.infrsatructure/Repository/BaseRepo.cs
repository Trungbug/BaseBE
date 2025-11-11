using Misa.demo.core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Microsoft.Extensions.Configuration;


namespace Misa.infrsatructure.Repository
{
    public class BaseRepo<T> : IBaseRepo<T>
    {
        protected readonly string connectionString;


        public BaseRepo(IConfiguration config)
        {
            connectionString = config.GetConnectionString("ConnectionString");
        }

        public int Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public T Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Insert(T enity)
        {
            throw new NotImplementedException();
        }

        public int Update(T enity, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
