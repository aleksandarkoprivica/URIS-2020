using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Options;
using UserService.Helpers;

namespace UserService.DAL
{
    public abstract class ConnectionManager
    {
        private string _connectionStringA;
        protected SqlConnection connection;
        public ConnectionManager(IOptions<AppSettings> settings)
        {
            _connectionStringA = settings.Value.ConnectionStrings;
        }
        public void Create()
        {
            if (connection == null)
            {
                connection = new SqlConnection(_connectionStringA);
                connection.Open();
               
            }
        }

    public void Close()
        {
            if(connection != null)
            {
                connection.Close();
                connection = null;
            }
        }
       
    }
}
