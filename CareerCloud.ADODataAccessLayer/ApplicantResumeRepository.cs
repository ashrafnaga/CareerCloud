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

    public class ApplicantResumeRepository : InitConnection, IDataRepository<ApplicantResumePoco>
    {
        public ApplicantResumeRepository() : base() { }

        public void Add(params ApplicantResumePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO [dbo].[Applicant_Resumes] 
(Id, Applicant, Resume, Last_Updated) VALUES (@Id, @Applicant, @Resume, @Last_Updated)";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Applicant", item.Applicant);
                comm.Parameters.AddWithValue("@Resume", item.Resume);
                comm.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Id, Applicant, Resume, Last_Updated FROM [dbo].[Applicant_Resumes]";

            SqlDataReader reader = comm.ExecuteReader();
            IList<ApplicantResumePoco> items = new List<ApplicantResumePoco>();
            while (reader.Read())
            {
                ApplicantResumePoco item = new ApplicantResumePoco();
                item.Id = reader.GetGuid(0);
                item.Applicant = reader.GetGuid(1);
                item.Resume = reader.GetString(2);
                item.LastUpdated = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3);

                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[Applicant_Resumes] WHERE Id = @Id";
                comm.Parameters.AddWithValue("@Id", item.Id);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[Applicant_Resumes] 
SET Applicant = @Applicant, Resume = @Resume, Last_Updated = @Last_Updated WHERE Id = @Id";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Applicant", item.Applicant);
                comm.Parameters.AddWithValue("@Resume", item.Resume);
                comm.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
