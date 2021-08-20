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

    public class CompanyDescriptionRepository : InitConnection, IDataRepository<CompanyDescriptionPoco>
    {
        public CompanyDescriptionRepository() : base() { }

        public void Add(params CompanyDescriptionPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO [dbo].[Company_Descriptions] 
(Id, Company, LanguageID, Company_Name, Company_Description) 
VALUES (@Id, @Company, @LanguageID, @Company_Name, @Company_Description)";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Company", item.Company);
                comm.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                comm.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                comm.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Id, Company, LanguageID, Company_Name, Company_Description, Time_Stamp FROM [dbo].[Company_Descriptions]";
            
            SqlDataReader reader = comm.ExecuteReader();
            IList<CompanyDescriptionPoco> items = new List<CompanyDescriptionPoco>();
            while (reader.Read())
            {
                CompanyDescriptionPoco item = new CompanyDescriptionPoco();
                item.Id = reader.GetGuid(0);
                item.Company = reader.GetGuid(1);
                item.LanguageId = reader.GetString(2);
                item.CompanyName = reader.GetString(3);
                item.CompanyDescription = reader.GetString(4);
                item.TimeStamp = (byte[])reader[5];

                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[Company_Descriptions] WHERE Id = @Id";
                comm.Parameters.AddWithValue("@Id", item.Id);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[Company_Descriptions] 
SET Company = @Company, LanguageID = @LanguageID, Company_Name = @Company_Name, Company_Description = @Company_Description WHERE Id = @Id";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Company", item.Company);
                comm.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                comm.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                comm.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
