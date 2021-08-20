using CareerCloud.ADODataAccessLayer.DBConnect;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class SystemLanguageCodeRepository : InitConnection, IDataRepository<SystemLanguageCodePoco>
    {
        public SystemLanguageCodeRepository() : base() { }

        public void Add(params SystemLanguageCodePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO [dbo].[System_Language_Codes] 
(LanguageID, Name, Native_Name) VALUES (@LanguageID, @Name, @Native_Name)";

                comm.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                comm.Parameters.AddWithValue("@Name", item.Name);
                comm.Parameters.AddWithValue("@Native_Name", item.NativeName);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT LanguageID, Name, Native_Name FROM [dbo].[System_Language_Codes]";

            SqlDataReader reader = comm.ExecuteReader();
            IList<SystemLanguageCodePoco> items = new List<SystemLanguageCodePoco>();
            while (reader.Read())
            {
                SystemLanguageCodePoco item = new SystemLanguageCodePoco();
                item.LanguageID = reader.GetString(0);
                item.Name = reader.GetString(1);
                item.NativeName = reader.GetString(2);

                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[System_Language_Codes] WHERE LanguageID = @LanguageID";
                comm.Parameters.AddWithValue("@LanguageID", item.LanguageID);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[System_Language_Codes] SET Name = @Name,Native_Name = @Native_Name WHERE LanguageID = @LanguageID";

                comm.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                comm.Parameters.AddWithValue("@Name", item.Name);
                comm.Parameters.AddWithValue("@Native_Name", item.NativeName);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
