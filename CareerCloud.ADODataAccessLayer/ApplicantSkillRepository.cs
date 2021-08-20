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

    public class ApplicantSkillRepository : InitConnection, IDataRepository<ApplicantSkillPoco>
    {
        public ApplicantSkillRepository() : base() { }

        public void Add(params ApplicantSkillPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO [dbo].[Applicant_Skills]
(Id, Applicant, Skill, Skill_Level, Start_Month, Start_Year, End_Month, End_Year) 
VALUES (@Id, @Applicant, @Skill, @Skill_Level, @Start_Month, @Start_Year, @End_Month, @End_Year)";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Applicant", item.Applicant);
                comm.Parameters.AddWithValue("@Skill", item.Skill);
                comm.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Id, Applicant, Skill, Skill_Level, Start_Month, Start_Year, 
End_Month, End_Year, Time_Stamp FROM [dbo].[Applicant_Skills]";
            
            SqlDataReader reader = comm.ExecuteReader();
            IList<ApplicantSkillPoco> items = new List<ApplicantSkillPoco>();
            while (reader.Read())
            {
                ApplicantSkillPoco item = new ApplicantSkillPoco();
                item.Id = reader.GetGuid(0);
                item.Applicant = reader.GetGuid(1);
                item.Skill = reader.GetString(2);
                item.SkillLevel = reader.GetString(3);
                item.StartMonth = reader.GetByte(4);
                item.StartYear = reader.GetInt32(5);
                item.EndMonth = reader.GetByte(6);
                item.EndYear = reader.GetInt32(7);
                item.TimeStamp = (byte[])reader[8];

                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[Applicant_Skills] WHERE Id = @Id";
                comm.Parameters.AddWithValue("@Id", item.Id);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[Applicant_Skills] 
SET Applicant = @Applicant, Skill = @Skill, Skill_Level = @Skill_Level, Start_Month = @Start_Month, 
Start_Year = @Start_Year, End_Month = @End_Month, End_Year = @End_Year WHERE Id = @Id";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Applicant", item.Applicant);
                comm.Parameters.AddWithValue("@Skill", item.Skill);
                comm.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
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
