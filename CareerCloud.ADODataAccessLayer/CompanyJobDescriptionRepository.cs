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

    public class CompanyJobDescriptionRepository : InitConnection, IDataRepository<CompanyJobDescriptionPoco>
    {
        public CompanyJobDescriptionRepository() : base() { }

        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO [dbo].[Company_Jobs_Descriptions] 
(Id, Job, Job_Name, Job_Descriptions) VALUES (@Id, @Job, @Job_Name, @Job_Descriptions)";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Job", item.Job);
                comm.Parameters.AddWithValue("@Job_Name", item.JobName);
                comm.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Id, Job, Job_Name, Job_Descriptions, Time_Stamp FROM [dbo].[Company_Jobs_Descriptions]";
            SqlDataReader reader = comm.ExecuteReader();
            IList<CompanyJobDescriptionPoco> items = new List<CompanyJobDescriptionPoco>();
            while (reader.Read())
            {
                CompanyJobDescriptionPoco item = new CompanyJobDescriptionPoco();
                item.Id = reader.GetGuid(0);
                item.Job = reader.GetGuid(1);
                item.JobName = reader.GetString(2);
                item.JobDescriptions = reader.GetString(3);
                item.TimeStamp = (byte[])reader[4];

                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[Company_Jobs_Descriptions] WHERE Id = @Id";
                comm.Parameters.AddWithValue("@Id", item.Id);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[Company_Jobs_Descriptions] 
SET Job = @Job, Job_Name =@Job_Name, Job_Descriptions = @Job_Descriptions WHERE Id = @Id";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Job", item.Job);
                comm.Parameters.AddWithValue("@Job_Name", item.JobName);
                comm.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
