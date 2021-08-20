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

    public class CompanyJobEducationRepository : InitConnection, IDataRepository<CompanyJobEducationPoco>
    {
        public CompanyJobEducationRepository() : base() { }

        public void Add(params CompanyJobEducationPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO [dbo].[Company_Job_Educations] 
(Id, Job, Major, Importance) VALUES (@Id, @Job, @Major, @Importance)";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Job", item.Job);
                comm.Parameters.AddWithValue("@Major", item.Major);
                comm.Parameters.AddWithValue("@Importance", item.Importance);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Id, Job, Major, Importance, Time_Stamp FROM [dbo].[Company_Job_Educations]";

            SqlDataReader reader = comm.ExecuteReader();
            IList<CompanyJobEducationPoco> items = new List<CompanyJobEducationPoco>();
            while (reader.Read())
            {
                CompanyJobEducationPoco item = new CompanyJobEducationPoco();
                item.Id = reader.GetGuid(0);
                item.Job = reader.GetGuid(1);
                item.Major = reader.GetString(2);
                item.Importance = reader.GetInt16(3);
                item.TimeStamp = (byte[])reader[4];

                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<CompanyJobEducationPoco> GetList(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public CompanyJobEducationPoco GetSingle(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobEducationPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[Company_Job_Educations] WHERE Id = @Id";
                comm.Parameters.AddWithValue("@Id", item.Id);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params CompanyJobEducationPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[Company_Job_Educations] SET Job = @Job, Major = @Major, 
Importance = @Importance WHERE Id = @Id";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Job", item.Job);
                comm.Parameters.AddWithValue("@Major", item.Major);
                comm.Parameters.AddWithValue("@Importance", item.Importance); ;

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
