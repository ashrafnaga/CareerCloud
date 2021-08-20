namespace CareerCloud.ADODataAccessLayer
{
    using CareerCloud.ADODataAccessLayer.DBConnect;
    using CareerCloud.DataAccessLayer;
    using CareerCloud.Pocos;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Linq.Expressions;

    public class SecurityLoginsLogRepository : InitConnection, IDataRepository<SecurityLoginsLogPoco>
    {
        public SecurityLoginsLogRepository() : base() { }

        public void Add(params SecurityLoginsLogPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO [dbo].[Security_Logins_Log] 
(Id, Login, Source_IP, Logon_Date, Is_Succesful) VALUES (@Id, @Login, @Source_IP, @Logon_Date, @Is_Succesful)";


                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Login", item.Login);
                comm.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                comm.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                comm.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Id, Login, Source_IP, Logon_Date, Is_Succesful FROM [dbo].[Security_Logins_Log]";
            
            SqlDataReader reader = comm.ExecuteReader();
            IList<SecurityLoginsLogPoco> items = new List<SecurityLoginsLogPoco>();
            while (reader.Read())
            {
                SecurityLoginsLogPoco item = new SecurityLoginsLogPoco();
                item.Id = reader.GetGuid(0);
                item.Login = reader.GetGuid(1);
                item.SourceIP = reader.GetString(2);
                item.LogonDate = reader.GetDateTime(3);
                item.IsSuccesful = reader.GetBoolean(4);


                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[Security_Logins_Log] WHERE Id = @Id";
                comm.Parameters.AddWithValue("@Id", item.Id);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[Security_Logins_Log] 
SET Login = @Login, Source_IP = @Source_IP, Logon_Date = @Logon_Date, Is_Succesful = @Is_Succesful WHERE Id = @Id";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Login", item.Login);
                comm.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                comm.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                comm.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
