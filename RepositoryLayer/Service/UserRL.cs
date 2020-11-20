using CommonLayer.Exceptions;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Identity;
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
    public class UserRL : IUserRL
    {
        private UserManager<IdentityUser> _userManger;

        /// <summary>
        /// create field for configuration 
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Database Connection Variable
        /// </summary>
        /*private SqlConnection sqlConnectionVariable;*/

        private static readonly string ConnectionDeclaration = "Server=.; Database=BookStoreDatabase; Trusted_Connection=true;MultipleActiveResultSets=True";

        SqlConnection sqlConnectionVariable = new SqlConnection(ConnectionDeclaration);

        public UserRL(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            this.configuration = configuration;
            this._userManger = userManager;
        }

        /// <summary>
        /// Function For Register User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        public async Task<RUserRegisterModel> RegisterUser(UserRegisterModel user)
        {
            try
            {
                    var identityUser = new IdentityUser
                    {
                        Email = user.EmailId,
                        UserName = user.FirstName,
                    };

                     var result = await _userManger.CreateAsync(identityUser, user.Password);

                if (result.Succeeded)
                {

                    int status = 0;
                    string Role = "User";
                    RUserRegisterModel usermodel = new RUserRegisterModel();
                    user.Password = Encrypt(user.Password).ToString();
                    SqlCommand sqlCommand = new SqlCommand("spUserRegistration", this.sqlConnectionVariable);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@FirstName", user.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", user.LastName);
                    sqlCommand.Parameters.AddWithValue("@UserRole", Role);
                    sqlCommand.Parameters.AddWithValue("@EmailId", user.EmailId);
                    sqlCommand.Parameters.AddWithValue("@Password", user.Password);
                    /*sqlCommand.Parameters.AddWithValue("@Locality", user.Locality);
                    sqlCommand.Parameters.AddWithValue("@City", user.City);
                    sqlCommand.Parameters.AddWithValue("@State", user.State);*/
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                    /*sqlCommand.Parameters.AddWithValue("@PinCode", user.PinCode);
                    sqlCommand.Parameters.AddWithValue("@LandMark", user.LandMark);*/
                    this.sqlConnectionVariable.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        status = sqlDataReader.GetInt32(0);
                        usermodel.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                        usermodel.CreatedDate = sqlDataReader["ModificationDate"].ToString();
                        usermodel.UserRole = Role;
                        if (status > 0)
                        {
                            return DataCopy(usermodel, user);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                return null;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }


        /// <summary>
        /// Function For Register User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        public async Task<RUserLoginModel> UserLogin(UserLoginModel user)
        {
            try
            {
                var users = await _userManger.FindByEmailAsync(user.EmailId);

                if (user == null)
                {
                    throw new Exception(UserException.ExceptionType.INVALID_EMAIL_IDENTITY.ToString());
                }

                var result = await _userManger.CheckPasswordAsync(users, user.Password);

                if (!result)
                {
                    throw new Exception(UserException.ExceptionType.INVALID_PASSWORD_IDENTITY.ToString());
                }

                    int status = 0;
                    RUserLoginModel usermodel = new RUserLoginModel();
                    user.Password = Encrypt(user.Password).ToString();
                    SqlCommand sqlCommand = new SqlCommand("spUserLogin", this.sqlConnectionVariable);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@EmailId", user.EmailId);
                    sqlCommand.Parameters.AddWithValue("@Password", user.Password);
                    this.sqlConnectionVariable.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        status = sqlDataReader.GetInt32(0);
                        usermodel.UserId = Convert.ToInt32(sqlDataReader["UserID"]);
                        usermodel.CreatedDate = sqlDataReader["ModificationDate"].ToString();
                        usermodel.FirstName = sqlDataReader["FirstName"].ToString();
                        usermodel.LastName = sqlDataReader["LastName"].ToString();
                        usermodel.UserRole = sqlDataReader["Role"].ToString();
                        usermodel.EmailId = user.EmailId;
                    if (status > 0)
                        {
                            return usermodel;
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


        private RUserRegisterModel DataCopy(RUserRegisterModel usermodel, UserRegisterModel user)
        {
            usermodel.FirstName = user.FirstName;
            usermodel.LastName = user.LastName;
            usermodel.EmailId = user.EmailId;
            return usermodel;
        }


        public RUserLoginModel ForgetPassword(String EmailId)
        {
            try
            {
                
                RUserLoginModel rUser = new RUserLoginModel();
                SqlCommand sqlCommand = new SqlCommand("spEmailCheck", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EmailId", EmailId);
                this.sqlConnectionVariable.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    rUser.UserId = sqlDataReader.GetInt32(0);
                    if (rUser.UserId > 0)
                    {
                        rUser.EmailId = EmailId;
                        rUser.UserRole = sqlDataReader["Role"].ToString();
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

        public RUserLoginModel ResetPassword(string PassWord, string email)
        {
            try
            {

                RUserLoginModel rUser = new RUserLoginModel();
                PassWord = Encrypt(PassWord).ToString();
                SqlCommand sqlCommand = new SqlCommand("spResetPassWord", this.sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@email", email);
                sqlCommand.Parameters.AddWithValue("@PassWord", PassWord);
                this.sqlConnectionVariable.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    rUser.UserId = sqlDataReader.GetInt32(0);
                    if (rUser.UserId > 0)
                    {
                        rUser.UserId = Convert.ToInt32(sqlDataReader["UserID"]);
                        rUser.CreatedDate = sqlDataReader["ModificationDate"].ToString();
                        rUser.FirstName = sqlDataReader["FirstName"].ToString();
                        rUser.LastName = sqlDataReader["LastName"].ToString();
                        rUser.UserRole = sqlDataReader["Role"].ToString();
                        rUser.EmailId = sqlDataReader["EmailId"].ToString();
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

        /// <summary>
        /// Declaration of encrypt method
        /// </summary>
        /// <param name="originalString">Passing password string</param>
        /// <returns>return string</returns>
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
