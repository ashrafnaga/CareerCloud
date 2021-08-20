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

    public class CompanyLocationRepository : InitConnection, IDataRepository<CompanyLocationPoco>
    {
        public CompanyLocationRepository() : base() { }


        public void Add(params CompanyLocationPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO [dbo].[Company_Locations] 
(Id, Company, Country_Code, State_Province_Code, Street_Address, City_Town, Zip_Postal_Code) 
VALUES (@Id, @Company, @Country_Code, @State_Province_Code, @Street_Address, @City_Town, @Zip_Postal_Code)";


                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Company", item.Company);
                comm.Parameters.AddWithValue("@Country_Code", item.CountryCode);
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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Id, Company, Country_Code, State_Province_Code, Street_Address, City_Town, 
Zip_Postal_Code, Time_Stamp FROM [dbo].[Company_Locations]";
            
            SqlDataReader reader = comm.ExecuteReader();
            IList<CompanyLocationPoco> items = new List<CompanyLocationPoco>();
            while (reader.Read())
            {
                CompanyLocationPoco item = new CompanyLocationPoco();
                item.Id = reader.GetGuid(0);
                item.Company = reader.GetGuid(1);
                item.CountryCode = reader.GetString(2);
                item.Province = reader.GetString(3);
                item.Street = reader.GetString(4);
                item.City = reader.IsDBNull(5) ? (string)null : reader.GetString(5);
                item.PostalCode = reader.IsDBNull(6) ? (string)null : reader.GetString(6);

                item.TimeStamp = (byte[])reader[7];

                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[Company_Locations] WHERE Id = @Id";
                comm.Parameters.AddWithValue("@Id", item.Id);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[Company_Locations] SET Company = @Company, Country_Code = @Country_Code, 
State_Province_Code = @State_Province_Code, Street_Address = @Street_Address, City_Town = @City_Town, 
Zip_Postal_Code = @Zip_Postal_Code WHERE Id = @Id";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Company", item.Company);
                comm.Parameters.AddWithValue("@Country_Code", item.CountryCode);
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
