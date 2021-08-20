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

    public class CompanyJobRepository : InitConnection, IDataRepository<CompanyJobPoco>
    {
        public CompanyJobRepository() : base() { }

        public void Add(params CompanyJobPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO [dbo].[Company_Jobs] 
(Id, Company, Profile_Created, Is_Inactive, Is_Company_Hidden) VALUES (@Id, @Company, @Profile_Created, @Is_Inactive, @Is_Company_Hidden)";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Company", item.Company);
                comm.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                comm.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                comm.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Id, Company, Profile_Created, Is_Inactive, Is_Company_Hidden, Time_Stamp FROM [dbo].[Company_Jobs]";
            
            SqlDataReader reader = comm.ExecuteReader();
            IList<CompanyJobPoco> items = new List<CompanyJobPoco>();
            while (reader.Read())
            {
                CompanyJobPoco item = new CompanyJobPoco();
                item.Id = reader.GetGuid(0);
                item.Company = reader.GetGuid(1);
                item.ProfileCreated = reader.GetDateTime(2);
                item.IsInactive = reader.GetBoolean(3);
                item.IsCompanyHidden = reader.GetBoolean(4);
                item.TimeStamp = (byte[])reader[5];

                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            _connection.Open();
            foreach (CompanyJobPoco poco in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[Company_Jobs] WHERE Id = @Id";
                comm.Parameters.AddWithValue("@Id", poco.Id);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params CompanyJobPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[Company_Jobs] 
SET Company = @Company, Profile_Created = @Profile_Created, Is_Inactive = @Is_Inactive, Is_Company_Hidden = @Is_Company_Hidden WHERE Id = @Id";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Company", item.Company);
                comm.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                comm.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                comm.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
