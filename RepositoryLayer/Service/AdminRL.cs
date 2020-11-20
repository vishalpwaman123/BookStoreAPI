using CommonLayer.Exceptions;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class AdminRL : IAdminRL
    {
        /// <summary>
        /// create field for configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initialzes the memory and inject the configuration interface 
        /// </summary>
        /// <param name="configuration"></param>
        public AdminRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        /// <summary>
        /// Admin signup method
        /// </summary>
        /// <param name="adminRegisterModel"></param>
        /// <returns></returns>
        public async Task<RAdminRegisterModel> RegisterAdmin(AdminRegisterModel adminRegisterModel)
        {
            try
            {
                    String Password = adminRegisterModel.Password;
                    var userType = "admin";
                    DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                    SqlConnection sqlConnection = databaseConnection.GetConnection();
                    adminRegisterModel.Password = Encrypt(adminRegisterModel.Password).ToString();
                    SqlCommand sqlCommand = databaseConnection.GetCommand("spAdminRegistration", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@AdminName", adminRegisterModel.AdminName);
                    sqlCommand.Parameters.AddWithValue("@AdminEmailId", adminRegisterModel.AdminEmailId);
                    sqlCommand.Parameters.AddWithValue("@Password", adminRegisterModel.Password);
                    sqlCommand.Parameters.AddWithValue("@Gender", adminRegisterModel.Gender);
                    sqlCommand.Parameters.AddWithValue("@Role", userType);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    var userData = new RAdminRegisterModel();
                while (sqlDataReader.Read())
                {
                    //userData = new RAdminLoginModel();
                    int status = sqlDataReader.GetInt32(0);
                    if (status == 0)
                    {
                        return null;
                    }
   
                        userData.AdminId = (int)sqlDataReader["AdminID"];
                        userData.AdminName = sqlDataReader["AdminName"].ToString();
                        userData.AdminEmailId = sqlDataReader["AdminEmailId"].ToString();
                        userData.Role = sqlDataReader["Role"].ToString();
                        userData.Gender = sqlDataReader["Gender"].ToString();
                        userData.CreatedDate = sqlDataReader["ModificationDate"].ToString();
                    
                }
                    
                    if (userData.AdminEmailId != null)
                    {
                        return userData;
                    }
                    else
                    {
                        return null;
                    }
                
               
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }


        /// <summary>
        /// Admin login method
        /// </summary>
        /// <param name="adminLoginModel"></param>
        /// <returns></returns>
        public async Task<RAdminLoginModel> AdminLogin(AdminLoginModel adminLoginModel)
        {
            try
            {
                

                adminLoginModel.Password = Encrypt(adminLoginModel.Password).ToString();
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                SqlConnection sqlConnection = databaseConnection.GetConnection();
                SqlCommand sqlCommand = databaseConnection.GetCommand("spAdminLogin", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@EmailId", adminLoginModel.Email);
                sqlCommand.Parameters.AddWithValue("@Password", adminLoginModel.Password);
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                var userData = new RAdminLoginModel();
                while (sqlDataReader.Read())
                {
                    int status = sqlDataReader.GetInt32(0);
                    if(status == 0)
                    {
                        return null;
                    }
                    userData.AdminId = (int)sqlDataReader["AdminID"];
                    userData.AdminName = sqlDataReader["AdminName"].ToString();
                    userData.AdminEmailId = sqlDataReader["AdminEmailId"].ToString();
                    userData.Role = sqlDataReader["Role"].ToString();
                    userData.Gender = sqlDataReader["Gender"].ToString();
                    userData.CreatedDate = sqlDataReader["ModificationDate"].ToString();
                }

                if (userData!= null)
                {
                    return userData;
                }
                return null;
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public RAdminLoginModel ForgetPassword(String EmailId)
        {
            try
            {

                RAdminLoginModel rUser = new RAdminLoginModel();
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                SqlConnection sqlConnection = databaseConnection.GetConnection();
                SqlCommand sqlCommand = databaseConnection.GetCommand("spAdminEmailCheck", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@EmailId", EmailId);
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    rUser.AdminId = sqlDataReader.GetInt32(0);
                    if (rUser.AdminId > 0)
                    {
                        rUser.AdminEmailId = EmailId;
                        rUser.Role = sqlDataReader["Role"].ToString();
                        return rUser;
                    }
                    else
                    {
                        return null;
                    }
                }

                return null;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public RAdminLoginModel ResetPassword(string PassWord, string email)
        {
            try
            {

                RAdminLoginModel rUser = new RAdminLoginModel();
                PassWord = Encrypt(PassWord).ToString();
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                SqlConnection sqlConnection = databaseConnection.GetConnection();
                SqlCommand sqlCommand = databaseConnection.GetCommand("spResetAdminPassWord", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@email", email);
                sqlCommand.Parameters.AddWithValue("@PassWord", PassWord);
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    rUser.AdminId = sqlDataReader.GetInt32(0);
                    if (rUser.AdminId > 0)
                    {
                        rUser.CreatedDate = sqlDataReader["ModificationDate"].ToString();
                        rUser.AdminName = sqlDataReader["AdminName"].ToString();
                        rUser.Gender = sqlDataReader["Gender"].ToString();
                        rUser.Role = sqlDataReader["Role"].ToString();
                        rUser.AdminEmailId = sqlDataReader["AdminEmailId"].ToString();
                        return rUser;
                    }
                    else
                    {
                        return null;
                    }
                }

                return null;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string Encrypt(string originalString)
        {
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool");
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException("The string which needs to be encrypted can not be null.");
            }

            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes),
                CryptoStreamMode.Write);
            var writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        /// <summary>
        /// Declaration of decrypt method
        /// </summary>
        /// <param name="encryptedString">passing string</param>
        /// <returns>return string</returns>
        public static string Decrypt(string encryptedString)
        {
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool");
            if (String.IsNullOrEmpty(encryptedString))
            {
                throw new ArgumentNullException("The string which needs to be decrypted can not be null.");
            }

            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream(Convert.FromBase64String(encryptedString));
            var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes),
                CryptoStreamMode.Read);
            var reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }

    }
}
