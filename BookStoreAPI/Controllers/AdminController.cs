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
    public class AdminController : ControllerBase
    {

        //UserBl Reference.
        private IAdminBL adminBL;

        //IConfiguration Reference for JWT.
        private IConfiguration configuration;

        /// <summary>
        /// Constructor for UserBL Reference.
        /// </summary>
        /// <param name="userBL"></param>
        public AdminController(IAdminBL userBL, IConfiguration configuration)
        {
            this.adminBL = userBL;
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
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterModel adminRegisterModel)
        {
            try
            {
                //Throws Custom Exception When Fields are Null.
                if (adminRegisterModel.AdminName == null || adminRegisterModel.Password == null || adminRegisterModel.AdminEmailId == null || adminRegisterModel.PhoneNumber == null )
                {
                    throw new Exception(AdminExceptions.ExceptionType.NULL_EXCEPTION.ToString());
                }

                //Throws Custom Exception When Fields are Empty Strings.
                if (adminRegisterModel.AdminName == "" || adminRegisterModel.Password == "" || adminRegisterModel.AdminEmailId == "" || adminRegisterModel.PhoneNumber == "")
                {
                    throw new Exception(AdminExceptions.ExceptionType.EMPTY_EXCEPTION.ToString());
                }

                var data = await adminBL.RegisterAdmin(adminRegisterModel);

                if (data != null)
                {
                    return Ok(new { status = "True", message = "Register Successfully", data });
                }
                else
                {
                    return Conflict(new { status = "False", message = "Email Already Present" });
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
        public async Task<IActionResult> AdminLogin([FromBody] AdminLoginModel adminLoginModel)
        {
            try
            {
                if (adminLoginModel.Password == null || adminLoginModel.Email == null )
                {
                    throw new Exception(AdminExceptions.ExceptionType.NULL_EXCEPTION.ToString());
                }

                //Throws Custom Exception When Fields are Empty Strings.
                if (adminLoginModel.Password == "" || adminLoginModel.Email == "")
                {
                    throw new Exception(AdminExceptions.ExceptionType.EMPTY_EXCEPTION.ToString());
                }

                RAdminLoginModel data = await this.adminBL.AdminLogin(adminLoginModel);
                if (data != null)
                {
                    data.Token = this.CreateToken(data, "authenticate user role");
                    return this.Ok(new { status = "True", message = "Login Successfully", data });
                }
                else
                {
                    return this.NotFound(new { status = "False", message = "Login UnSuccessfully" });
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
                RAdminLoginModel responseUser = null;

                if (!EmailId.Equals(null))
                {

                    //Throws Custom Exception When Fields are Null.
                    if (EmailId == null)
                    {
                        throw new Exception(AdminExceptions.ExceptionType.NULL_EXCEPTION.ToString());
                    }

                    //Throws Custom Exception When Fields are Empty Strings.
                    if (EmailId == "")
                    {
                        throw new Exception(UserException.ExceptionType.EMPTY_EXCEPTION.ToString());
                    }

                    responseUser = adminBL.ForgetPassword(EmailId);
                }
                else
                {
                    throw new Exception(UserException.ExceptionType.INVALID_ROLE_EXCEPTION.ToString());
                }

                if (!responseUser.Equals(null))
                {
                    var token = this.CreateToken(responseUser, "Reset Password");

                    string message = "\n EMAIL ID: \t" + Convert.ToString(responseUser.AdminEmailId) +
                  "\n ROLE: \t" + Convert.ToString(responseUser.Role) +
                  "\n API TOKEN : \t" + token +
                  "\nClick Next Link :http://localhost:4200/ResetPassword/" + token+
                  "\n Thank You";


                    senderObject.Send(message);

                    bool Success = true;
                    var Message = "Send Token In User Email Id Successfull";
                    return Ok(new { Success, Message, });
                }
                else
                {
                    bool Success = false;
                    var Message = "Operation Unsucessfull";
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
                RAdminLoginModel responseUser = null;

                if (!PassWord.Equals(null) && !ConfirmPassWord.Equals(null))
                {
                    if (PassWord != ConfirmPassWord)
                    {
                        throw new Exception(AdminExceptions.ExceptionType.INVALID_MATCH_PASSWORD_EXCEPTION.ToString());
                    }

                    //Throws Custom Exception When Fields are Empty Strings.
                    if (PassWord == "" || ConfirmPassWord == "")
                    {
                        throw new Exception(UserException.ExceptionType.EMPTY_EXCEPTION.ToString());
                    }

                    //Throws Custom Exception When Fields are Empty Strings.
                    if (PassWord == null || ConfirmPassWord == null)
                    {
                        throw new Exception(UserException.ExceptionType.NULL_EXCEPTION.ToString());
                    }

                    var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Email").Value;
                    responseUser = adminBL.ResetPassword(PassWord, email);
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
        private string CreateToken(RAdminLoginModel responseModel, string type)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, responseModel.Role));
                claims.Add(new Claim("Email", responseModel.AdminEmailId.ToString()));
                claims.Add(new Claim("Id", responseModel.AdminId.ToString()));
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
