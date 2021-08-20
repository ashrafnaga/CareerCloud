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

    public class SecurityRoleRepository : InitConnection, IDataRepository<SecurityRolePoco>
    {
        public SecurityRoleRepository() : base() { }

        public void Add(params SecurityRolePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO [dbo].[Security_Roles] 
(Id, Role, Is_Inactive) VALUES (@Id, @Role, @Is_Inactive)";


                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Role", item.Role);
                comm.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Id, Role, Is_Inactive FROM [dbo].[Security_Roles]";
            
            SqlDataReader reader = comm.ExecuteReader();
            IList<SecurityRolePoco> items = new List<SecurityRolePoco>();
            while (reader.Read())
            {
                SecurityRolePoco item = new SecurityRolePoco();
                item.Id = reader.GetGuid(0);
                item.Role = reader.GetString(1);
                item.IsInactive = reader.GetBoolean(2);


                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityRolePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[Security_Roles] WHERE Id = @Id";
                comm.Parameters.AddWithValue("@Id", item.Id);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params SecurityRolePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[Security_Roles] 
SET Role = @Role, Is_Inactive = @Is_Inactive WHERE Id = @Id";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Role", item.Role);
                comm.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
