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

    public class ApplicantJobApplicationRepository : InitConnection, IDataRepository<ApplicantJobApplicationPoco>
    {
        public ApplicantJobApplicationRepository() : base() { }
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO[dbo].[Applicant_Job_Applications] 
(Id, Applicant, Job, Application_Date) VALUES (@Id, @Applicant, @Job, @Application_Date)";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Applicant", item.Applicant);
                comm.Parameters.AddWithValue("@Job", item.Job);
                comm.Parameters.AddWithValue("@Application_Date", item.ApplicationDate);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Id, Applicant, Job, Application_Date, Time_Stamp FROM [dbo].[Applicant_Job_Applications]";
            
            SqlDataReader reader = comm.ExecuteReader();
            IList<ApplicantJobApplicationPoco> items = new List<ApplicantJobApplicationPoco>();
            while (reader.Read())
            {
                ApplicantJobApplicationPoco item = new ApplicantJobApplicationPoco();
                item.Id = reader.GetGuid(0);
                item.Applicant = reader.GetGuid(1);
                item.Job = reader.GetGuid(2);
                item.ApplicationDate = reader.GetDateTime(3);
                item.TimeStamp = (byte[])reader[4];

                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[Applicant_Job_Applications] WHERE  Id = @Id";
                comm.Parameters.AddWithValue("@Id", item.Id);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[Applicant_Job_Applications] 
SET Applicant = @Applicant, Job = @Job, Application_Date = @Application_Date WHERE Id = @Id";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Applicant", item.Applicant);
                comm.Parameters.AddWithValue("@Job", item.Job);
                comm.Parameters.AddWithValue("@Application_Date", item.ApplicationDate);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
