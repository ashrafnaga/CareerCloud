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

    public class SystemCountryCodeRepository : InitConnection, IDataRepository<SystemCountryCodePoco>
    {
        public SystemCountryCodeRepository() : base() { }

        public void Add(params SystemCountryCodePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO [dbo].[System_Country_Codes] (Code, Name) VALUES (@Code, @Name)";

                comm.Parameters.AddWithValue("@Code", item.Code);
                comm.Parameters.AddWithValue("@Name", item.Name);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Code, Name FROM [dbo].[System_Country_Codes]";
            
            SqlDataReader reader = comm.ExecuteReader();
            IList<SystemCountryCodePoco> items = new List<SystemCountryCodePoco>();
            while (reader.Read())
            {
                SystemCountryCodePoco item = new SystemCountryCodePoco();
                item.Code = reader.GetString(0);
                item.Name = reader.GetString(1);

                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[System_Country_Codes] WHERE Code = @Code";
                comm.Parameters.AddWithValue("@Code", item.Code);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[System_Country_Codes] SET Name = @Name WHERE Code = @Code";

                comm.Parameters.AddWithValue("@Code", item.Code);
                comm.Parameters.AddWithValue("@Name", item.Name);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
