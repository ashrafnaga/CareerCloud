namespace CareerCloud.ADODataAccessLayer.DBConnect
{
    using Microsoft.Extensions.Configuration;
    using System.Data.SqlClient;
    using System.IO;

    public class InitConnection
    {
        protected readonly SqlConnection _connection;
        protected readonly string _connectionStr;

        public InitConnection()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connectionStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;

            _connection = new SqlConnection(_connectionStr);
        }


        ~InitConnection()
        {
            _connection.Close();
        }

    }
    
}
