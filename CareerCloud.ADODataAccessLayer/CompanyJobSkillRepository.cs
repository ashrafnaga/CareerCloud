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

    public class CompanyJobSkillRepository : InitConnection, IDataRepository<CompanyJobSkillPoco>
    {
        public CompanyJobSkillRepository() : base() { }

        public void Add(params CompanyJobSkillPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO [dbo].[Company_Job_Skills] 
(Id, Job, Skill, Skill_Level, Importance) VALUES (@Id, @Job, @Skill, @Skill_Level, @Importance)";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Job", item.Job);
                comm.Parameters.AddWithValue("@Skill", item.Skill);
                comm.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                comm.Parameters.AddWithValue("@Importance", item.Importance);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Id, Job, Skill, Skill_Level, Importance, Time_Stamp FROM [dbo].[Company_Job_Skills]";
            
            SqlDataReader reader = comm.ExecuteReader();
            IList<CompanyJobSkillPoco> items = new List<CompanyJobSkillPoco>();
            while (reader.Read())
            {
                CompanyJobSkillPoco item = new CompanyJobSkillPoco();
                item.Id = reader.GetGuid(0);
                item.Job = reader.GetGuid(1);
                item.Skill = reader.GetString(2);
                item.SkillLevel = reader.GetString(3);
                item.Importance = reader.GetInt32(4);
                item.TimeStamp = (byte[])reader[5];

                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[Company_Job_Skills] WHERE Id = @Id";
                comm.Parameters.AddWithValue("@Id", item.Id);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[Company_Job_Skills] 
SET Job = @Job, Skill = @Skill, Skill_Level = @Skill_Level, Importance = @Importance WHERE Id = @Id";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Job", item.Job);
                comm.Parameters.AddWithValue("@Skill", item.Skill);
                comm.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                comm.Parameters.AddWithValue("@Importance", item.Importance);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
