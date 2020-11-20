using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookStoreAPI.MSMQSender;
using BusinessLayer.Interface;
using CommonLayer.Exceptions;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        //UserBl Reference.
        private IUsersBL userBL;

        //IConfiguration Reference for JWT.
        private IConfiguration configuration;

        /// <summary>
        /// Constructor for UserBL Reference.
        /// </summary>
        /// <param name="userBL"></param>
        public UsersController(IUsersBL userBL, IConfiguration configuration)
        {
            this.userBL = userBL;
            this.configuration = configuration;
        }

        /// <summary>
        /// Declare Sender object
        /// </summary>
        Sender senderObject = new Sender();

        /// <summary>
        /// Function For Resgister User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        //[Route("Admin")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterModel userRegisterModel)
        {
            try
            {
                RUserRegisterModel responseUser = null;

                if (!userRegisterModel.Equals(null))
                {

                    //Throws Custom Exception When Fields are Null.
                    if (userRegisterModel.FirstName == null || userRegisterModel.Password == null || userRegisterModel.EmailId == null || userRegisterModel.LastName == null || userRegisterModel.Password == null )
                    {
                        throw new Exception(UserException.ExceptionType.NULL_EXCEPTION.ToString());
                    }

                    //Throws Custom Exception When Fields are Empty Strings.
                    if (userRegisterModel.FirstName == "" || userRegisterModel.Password == "" || userRegisterModel.EmailId == "" || userRegisterModel.LastName == "" || userRegisterModel.Password == "" )
                    {
                        throw new Exception(UserException.ExceptionType.EMPTY_EXCEPTION.ToString());
                    }

                    responseUser = await userBL.RegisterUser(userRegisterModel);
                }
                else
                {
                    throw new Exception(UserException.ExceptionType.INVALID_ROLE_EXCEPTION.ToString());
                }

                if (responseUser != null)
                {
                    bool Success = true;
                    var Message = "Registration Successfull";
                    return Ok(new { Success, Message, Data = responseUser });
                }
                else
                {
                    bool Success = false;
                    var Message = "User Already Exists";
                    return Conflict(new { Success, Message });
                }
            }
            catch (Exception exception)
            {
                bool Success = false;
                return BadRequest(new { Success, Message = exception.Message });
            }
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginModel userLoginModel)
        {
            try
            {
                RUserLoginModel responseUser = null;

                if (!userLoginModel.Equals(null))
                {

                    //Throws Custom Exception When Fields are Null.
                    if (userLoginModel.Password == null || userLoginModel.EmailId == null)
                    {
                        throw new Exception(UserException.ExceptionType.NULL_EXCEPTION.ToString());
                    }

                    //Throws Custom Exception When Fields are Empty Strings.
                    if (userLoginModel.Password == "" || userLoginModel.EmailId == "" )
                    {
                        throw new Exception(UserException.ExceptionType.EMPTY_EXCEPTION.ToString());
                    }

                    responseUser = await this.userBL.UserLogin(userLoginModel);
                }
                else
                {
                    throw new Exception(UserException.ExceptionType.INVALID_ROLE_EXCEPTION.ToString());
                }

                if (responseUser != null)
                {
                    responseUser.token = this.CreateToken(responseUser, "authenticate user role");
                    bool Success = true;
                    var Message = "Login Successfull";
                    return Ok(new { Success, Message, Data = responseUser });
                }
                else
                {
                    bool Success = false;
                    var Message = "User Login Failed";
                    return Conflict(new { Success, Message });
                }
            }
            catch (Exception exception)
            {
                bool Success = false;
                return BadRequest(new { Success, Message = exception.Message });
            }
        }


        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(String EmailId)
        {
            try
            {
                RUserLoginModel responseUser = null;

                if (!EmailId.Equals(null))
                {

                    //Throws Custom Exception When Fields are Null.
                    if (EmailId == null)
                    {
                        throw new Exception(UserException.ExceptionType.NULL_EXCEPTION.ToString());
                    }

                    //Throws Custom Exception When Fields are Empty Strings.
                    if (EmailId == "")
                    {
                        throw new Exception(UserException.ExceptionType.EMPTY_EXCEPTION.ToString());
                    }

                    responseUser = userBL.ForgetPassword(EmailId);
                }
                else
                {
                    throw new Exception(UserException.ExceptionType.INVALID_ROLE_EXCEPTION.ToString());
                }

                if (!responseUser.Equals(null))
                {
                    var token = this.CreateToken(responseUser, "Reset Password");

                    string message = "\n Email Id: \t" + Convert.ToString(responseUser.EmailId) +
                  "\n Role: \t" + Convert.ToString(responseUser.UserRole) +
                  "\n API TOKEN : \t" + token +
                  "\n Thank You";


                    senderObject.Send(message);

                    bool Success = true;
                    var Message = "Send Token In User Email Id Successfull";
                    return Ok(new { Success, Message, });
                }
                else
                {
                    bool Success = false;
                    var Message = "User Login Failed";
                    return Conflict(new { Success, Message });
                }
            }
            catch (Exception exception)
            {
                bool Success = false;
                return BadRequest(new { Success, Message = exception.Message });
            }
        }

        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string PassWord, string ConfirmPassWord)
        {
            try
            {
                RUserLoginModel responseUser = null;

                if (!PassWord.Equals(null)&& !ConfirmPassWord.Equals(null))
                {
                    if (PassWord != ConfirmPassWord)
                    {
                        throw new Exception(UserException.ExceptionType.INVALID_MATCH_PASSWORD_EXCEPTION.ToString());
                    }

                    //Throws Custom Exception When Fields are Empty Strings.
                    if (PassWord == ""&& ConfirmPassWord=="")
                    {
                        throw new Exception(UserException.ExceptionType.EMPTY_EXCEPTION.ToString());
                    }
                    var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Email").Value;
                    responseUser = userBL.ResetPassword(PassWord, email);
                }
                else
                {
                    throw new Exception(UserException.ExceptionType.NULL_EXCEPTION.ToString());
                }

                if (!responseUser.Equals(null))
                {
                    
                    bool Success = true;
                    var Message = "Reset Password Successfull";
                    return Ok(new { Success, Message, });
                }
                else
                {
                    bool Success = false;
                    var Message = "Reset Password Not Successfull";
                    return Conflict(new { Success, Message });
                }
            }
            catch (Exception exception)
            {
                bool Success = false;
                return BadRequest(new { Success, Message = exception.Message });
            }
        }

        //Method to create JWT token
        private string CreateToken(RUserLoginModel responseModel, string type)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, responseModel.UserRole));
                claims.Add(new Claim("Email", responseModel.EmailId.ToString()));
                claims.Add(new Claim("Id", responseModel.UserId.ToString()));
                claims.Add(new Claim("TokenType", type));

                var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                    configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCreds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
