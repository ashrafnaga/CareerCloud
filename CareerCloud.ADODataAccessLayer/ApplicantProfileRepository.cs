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

    public class ApplicantProfileRepository : InitConnection, IDataRepository<ApplicantProfilePoco>
    {
        public ApplicantProfileRepository() : base() { }

        public void Add(params ApplicantProfilePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles] 
(Id, Login, Current_Salary, Current_Rate, Currency, Country_Code, State_Province_Code, Street_Address, City_Town, Zip_Postal_Code) 
VALUES (@Id, @Login, @Current_Salary, @Current_Rate, @Currency, @Country_Code, @State_Province_Code, @Street_Address, @City_Town, @Zip_Postal_Code)";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Login", item.Login);
                comm.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                comm.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                comm.Parameters.AddWithValue("@Currency", item.Currency);
                comm.Parameters.AddWithValue("@Country_Code", item.Country);
                comm.Parameters.AddWithValue("@State_Province_Code", item.Province);
                comm.Parameters.AddWithValue("@Street_Address", item.Street);
                comm.Parameters.AddWithValue("@City_Town", item.City);
                comm.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Id, Login, Current_Salary, Current_Rate, Currency, Country_Code,
State_Province_Code, Street_Address, City_Town, Zip_Postal_Code, Time_Stamp FROM [dbo].[Applicant_Profiles]";

            SqlDataReader reader = comm.ExecuteReader();
            IList<ApplicantProfilePoco> items = new List<ApplicantProfilePoco>();
            while (reader.Read())
            {
                ApplicantProfilePoco item = new ApplicantProfilePoco();
                item.Id = reader.GetGuid(0);
                item.Login = reader.GetGuid(1);
                item.CurrentSalary = reader.GetDecimal(2);
                item.CurrentRate = reader.GetDecimal(3);
                item.Currency = reader.GetString(4);
                item.Country = reader.GetString(5);
                item.Province = reader.GetString(6);
                item.Street = reader.GetString(7);
                item.City = reader.GetString(8);
                item.PostalCode = reader.GetString(9);
                item.TimeStamp = (byte[])reader[10];

                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[Applicant_Profiles] WHERE Id = @Id";
                comm.Parameters.AddWithValue("@Id", item.Id);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[Applicant_Profiles] 
SET Login = @Login, Current_Salary =@Current_Salary, Current_Rate = @Current_Rate, Currency = @Currency, 
Country_Code = @Country_Code, State_Province_Code = @State_Province_Code, Street_Address = @Street_Address, 
City_Town = @City_Town, Zip_Postal_Code = @Zip_Postal_Code WHERE Id = @Id";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Login", item.Login);
                comm.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                comm.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                comm.Parameters.AddWithValue("@Currency", item.Currency);
                comm.Parameters.AddWithValue("@Country_Code", item.Country);
                comm.Parameters.AddWithValue("@State_Province_Code", item.Province);
                comm.Parameters.AddWithValue("@Street_Address", item.Street);
                comm.Parameters.AddWithValue("@City_Town", item.City);
                comm.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
