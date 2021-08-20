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

    public class SecurityLoginsRoleRepository : InitConnection, IDataRepository<SecurityLoginsRolePoco>
    {
        public SecurityLoginsRoleRepository() : base() { }

        public void Add(params SecurityLoginsRolePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO [dbo].[Security_Logins_Roles] 
(Id, Login, Role) VALUES (@Id, @Login, @Role)";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Login", item.Login);
                comm.Parameters.AddWithValue("@Role", item.Role);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Id, Login, Role, Time_Stamp FROM [dbo].[Security_Logins_Roles]";
            
            SqlDataReader reader = comm.ExecuteReader();
            IList<SecurityLoginsRolePoco> items = new List<SecurityLoginsRolePoco>();
            while (reader.Read())
            {
                SecurityLoginsRolePoco item = new SecurityLoginsRolePoco();
                item.Id = reader.GetGuid(0);
                item.Login = reader.GetGuid(1);
                item.Role = reader.GetGuid(2);
                item.TimeStamp = (byte[])reader[3];

                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[Security_Logins_Roles] WHERE Id = @Id";
                comm.Parameters.AddWithValue("@Id", item.Id);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[Security_Logins_Roles] SET Login = @Login, Role = @Role WHERE Id = @Id";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Login", item.Login);
                comm.Parameters.AddWithValue("@Role", item.Role);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
