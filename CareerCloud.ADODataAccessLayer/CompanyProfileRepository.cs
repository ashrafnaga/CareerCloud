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
    
    public class CompanyProfileRepository : InitConnection, IDataRepository<CompanyProfilePoco>
    {
        public CompanyProfileRepository() : base() { }

        public void Add(params CompanyProfilePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO [dbo].[Company_Profiles] 
(Id, Registration_Date, Company_Website, Contact_Phone, Contact_Name, Company_Logo) 
VALUES (@Id, @Registration_Date, @Company_Website, @Contact_Phone, @Contact_Name, @Company_Logo)";


                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                comm.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                comm.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                comm.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                comm.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Id, Registration_Date, Company_Website, Contact_Phone, Contact_Name, Company_Logo, Time_Stamp FROM [dbo].[Company_Profiles]";
            
            SqlDataReader reader = comm.ExecuteReader();
            IList<CompanyProfilePoco> items = new List<CompanyProfilePoco>();
            while (reader.Read())
            {
                CompanyProfilePoco item = new CompanyProfilePoco();
                item.Id = reader.GetGuid(0);
                item.RegistrationDate = reader.GetDateTime(1);
                item.CompanyWebsite = reader.IsDBNull(2) ? (string)null : reader.GetString(2);
                item.ContactPhone = reader.GetString(3);
                item.ContactName = reader.IsDBNull(4) ? (string)null : reader.GetString(4); ;
                item.CompanyLogo = reader.IsDBNull(5) ? (byte[])null : (byte[])reader[5];
                item.TimeStamp = (byte[])reader[6];

                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[Company_Profiles] WHERE Id = @Id";
                comm.Parameters.AddWithValue("@Id", item.Id);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[Company_Profiles] 
SET Registration_Date = @Registration_Date, Company_Website = @Company_Website, Contact_Phone = @Contact_Phone, 
Contact_Name = @Contact_Name, Company_Logo = @Company_Logo WHERE Id = @Id";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                comm.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                comm.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                comm.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                comm.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
    
}
