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

    public class ApplicantWorkHistoryRepository : InitConnection, IDataRepository<ApplicantWorkHistoryPoco>
    {
        public ApplicantWorkHistoryRepository() : base() { }

        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO [dbo].[Applicant_Work_History] 
(Id, Applicant, Company_Name, Country_Code, Location, Job_Title, Job_Description, Start_Month, Start_Year, End_Month, End_Year) 
VALUES(@Id, @Applicant, @Company_Name, @Country_Code, @Location, @Job_Title, @Job_Description, @Start_Month, @Start_Year, @End_Month, @End_Year)";


                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Applicant", item.Applicant);
                comm.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                comm.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                comm.Parameters.AddWithValue("@Location", item.Location);
                comm.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                comm.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                comm.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                comm.Parameters.AddWithValue("@Start_Year", item.StartYear);
                comm.Parameters.AddWithValue("@End_Month", item.EndMonth);
                comm.Parameters.AddWithValue("@End_Year", item.EndYear);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Id, Applicant, Company_Name, Country_Code, Location, Job_Title, Job_Description, 
Start_Month, Start_Year, End_Month, End_Year, Time_Stamp FROM [dbo].[Applicant_Work_History]";
            
            SqlDataReader reader = comm.ExecuteReader();
            IList<ApplicantWorkHistoryPoco> items = new List<ApplicantWorkHistoryPoco>();
            while (reader.Read())
            {
                ApplicantWorkHistoryPoco item = new ApplicantWorkHistoryPoco();
                item.Id = reader.GetGuid(0);
                item.Applicant = reader.GetGuid(1);
                item.CompanyName = reader.GetString(2);
                item.CountryCode = reader.GetString(3);
                item.Location = reader.GetString(4);
                item.JobTitle = reader.GetString(5);
                item.JobDescription = reader.GetString(6);
                item.StartMonth = reader.GetInt16(7);
                item.StartYear = reader.GetInt32(8);
                item.EndMonth = reader.GetInt16(9);
                item.EndYear = reader.GetInt32(10);
                item.TimeStamp = (byte[])reader[11];

                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[Applicant_Work_History] WHERE Id = @Id";
                comm.Parameters.AddWithValue("@Id", item.Id);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[Applicant_Work_History] 
SET Applicant = @Applicant, Company_Name = @Company_Name, Country_Code = @Country_Code, Location = @Location, 
Job_Title = @Job_Title, Job_Description = @Job_Description, Start_Month = @Start_Month, Start_Year = @Start_Year, 
End_Month = @End_Month, End_Year = @End_Year WHERE Id = @Id";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Applicant", item.Applicant);
                comm.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                comm.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                comm.Parameters.AddWithValue("@Location", item.Location);
                comm.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                comm.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                comm.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                comm.Parameters.AddWithValue("@Start_Year", item.StartYear);
                comm.Parameters.AddWithValue("@End_Month", item.EndMonth);
                comm.Parameters.AddWithValue("@End_Year", item.EndYear);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
