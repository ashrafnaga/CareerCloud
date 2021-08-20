namespace CareerCloud.ADODataAccessLayer
{
    using CareerCloud.ADODataAccessLayer.DBConnect;
    using CareerCloud.DataAccessLayer;
    using CareerCloud.Pocos;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;

    public class ApplicantEducationRepository : InitConnection, IDataRepository<ApplicantEducationPoco>
    {
        public ApplicantEducationRepository():base() { }
        
        public void Add(params ApplicantEducationPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                item.TimeStamp = BitConverter.GetBytes(DateTime.Now.ToBinary());

                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO[dbo].[Applicant_Educations] 
(Id, Applicant , Major, Certificate_Diploma, Start_Date, Completion_Date, Completion_Percent, Time_Stamp) 
VALUES (@Id,@Applicant,@Major,@CertificateDiploma,@StartDate,@CompletionDate,@CompletionPercent, DEFAULT)";
                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Applicant", item.Applicant);
                comm.Parameters.AddWithValue("@Major", item.Major);
                comm.Parameters.AddWithValue("@CertificateDiploma", item.CertificateDiploma);
                comm.Parameters.AddWithValue("@StartDate", item.StartDate);
                comm.Parameters.AddWithValue("@CompletionDate", item.CompletionDate);
                comm.Parameters.AddWithValue("@CompletionPercent ", item.CompletionPercent);
                //comm.Parameters.AddWithValue("@TimeStamp ", item.TimeStamp);

                comm.ExecuteNonQuery();
            }

            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Id, Applicant, Major, Certificate_Diploma, Start_Date, Completion_Date,
Completion_Percent, Time_Stamp FROM [dbo].[Applicant_Educations]";
            
            SqlDataReader reader = comm.ExecuteReader();
            IList<ApplicantEducationPoco> items = new List<ApplicantEducationPoco>();
            while (reader.Read())
            {
                ApplicantEducationPoco item = new ApplicantEducationPoco();
                item.Id = reader.GetGuid(0);
                item.Applicant = reader.GetGuid(1);
                item.Major = reader.GetString(2);
                item.CertificateDiploma = reader.GetString(3);
                item.StartDate = reader.GetDateTime(4);
                item.CompletionDate = reader.GetDateTime(5);
                item.CompletionPercent = reader.GetByte(6);
                item.TimeStamp = (byte[])reader[7];

                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            _connection.Open();
            foreach (ApplicantEducationPoco item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM [dbo].[Applicant_Educations] WHERE  Id = @Id";
                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[Applicant_Educations] 
SET Applicant = @Applicant, Major = @Major, Certificate_Diploma = @Certificate_Diploma, Start_Date = @Start_Date,
Completion_Date = @Completion_Date, Completion_Percent = @Completion_Percent WHERE  Id = @Id";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Applicant", item.Applicant);
                comm.Parameters.AddWithValue("@Major", item.Major);
                comm.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                comm.Parameters.AddWithValue("@Start_Date", item.StartDate);
                comm.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                comm.Parameters.AddWithValue("@Completion_Percent ", item.CompletionPercent);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
