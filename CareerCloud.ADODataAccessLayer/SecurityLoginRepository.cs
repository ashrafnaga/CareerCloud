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

    
    public class SecurityLoginRepository : InitConnection, IDataRepository<SecurityLoginPoco>
    {
        public SecurityLoginRepository() : base() { }
        public void Add(params SecurityLoginPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"INSERT INTO [dbo].[Security_Logins] 
(Id, Login, Password, Created_Date, Password_Update_Date, Agreement_Accepted_Date, Is_Locked, Is_Inactive, Email_Address, Phone_Number, Full_Name, Force_Change_Password, Prefferred_Language) 
VALUES (@Id, @Login, @Password, @Created_Date, @Password_Update_Date, @Agreement_Accepted_Date, @Is_Locked, @Is_Inactive, @Email_Address, @Phone_Number, @Full_Name, @Force_Change_Password, @Prefferred_Language)";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Login", item.Login);
                comm.Parameters.AddWithValue("@Password", item.Password);
                comm.Parameters.AddWithValue("@Created_Date", item.Created);
                comm.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                comm.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                comm.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                comm.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                comm.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                comm.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                comm.Parameters.AddWithValue("@Full_Name", item.FullName);
                comm.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                comm.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            _connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = _connection;
            comm.CommandText = @"SELECT Id, Login, Password, Created_Date, Password_Update_Date, Agreement_Accepted_Date, 
Is_Locked, Is_Inactive, Email_Address, Phone_Number, Full_Name, Force_Change_Password, Prefferred_Language, Time_Stamp FROM [dbo].[Security_Logins]";
            
            SqlDataReader reader = comm.ExecuteReader();
            IList<SecurityLoginPoco> items = new List<SecurityLoginPoco>();
            while (reader.Read())
            {
                SecurityLoginPoco item = new SecurityLoginPoco();
                item.Id = reader.GetGuid(0);
                item.Login = reader.GetString(1);
                item.Password = reader.GetString(2);
                item.Created = reader.GetDateTime(3);
                item.PasswordUpdate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);
                item.AgreementAccepted = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5);
                item.IsLocked = reader.GetBoolean(6);
                item.IsInactive = reader.GetBoolean(7);
                item.EmailAddress = reader.GetString(8);
                item.PhoneNumber = reader.IsDBNull(9) ? (string)null : reader.GetString(9);
                item.FullName = reader.GetString(10);
                item.ForceChangePassword = reader.GetBoolean(11);
                item.PrefferredLanguage = reader.IsDBNull(12) ? (string)null : reader.GetString(12);
                item.TimeStamp = (byte[])reader[13];

                items.Add(item);
            }
            _connection.Close();
            return items;
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).ToList();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            _connection.Open();
            foreach (SecurityLoginPoco poco in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"DELETE FROM[dbo].[Security_Logins] WHERE Id = @Id";
                comm.Parameters.AddWithValue("@Id", poco.Id);
                
                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = _connection;
                comm.CommandText = @"UPDATE [dbo].[Security_Logins] 
SET Login = @Login, Password = @Password, Created_Date = @Created_Date, Password_Update_Date = @Password_Update_Date
, Agreement_Accepted_Date = @Agreement_Accepted_Date, Is_Locked =@Is_Locked, Is_Inactive = @Is_Inactive
, Email_Address = @Email_Address, Phone_Number = @Phone_Number, Full_Name = @Full_Name
, Force_Change_Password = @Force_Change_Password, Prefferred_Language = @Prefferred_Language WHERE  [Id]= @Id";

                comm.Parameters.AddWithValue("@Id", item.Id);
                comm.Parameters.AddWithValue("@Login", item.Login);
                comm.Parameters.AddWithValue("@Password", item.Password);
                comm.Parameters.AddWithValue("@Created_Date", item.Created);
                comm.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                comm.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                comm.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                comm.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                comm.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                comm.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                comm.Parameters.AddWithValue("@Full_Name", item.FullName);
                comm.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                comm.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);

                comm.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
