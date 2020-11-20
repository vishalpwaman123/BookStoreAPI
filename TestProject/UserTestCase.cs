using BookStoreAPI.Controllers;
using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.RequestModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestProject
{
    public class UserTestCase
    {
        UserManager<IdentityUser> userManager;

        UsersController userController;

        IUsersBL userBL;

        IUserRL userRL;

        public IConfiguration configuration;

        /// <summary>
        /// initializes the fields
        /// </summary>
        public UserTestCase()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            this.configuration = configurationBuilder.Build();
            this.userRL = new UserRL(this.configuration, userManager);
            this.userBL = new UserBL(this.userRL);
            this.userController = new UsersController(this.userBL, configuration);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenGetAllEmptyUserFields_ShouldReturnBadRequestObjectResult()
        {
            UserRegisterModel data = new UserRegisterModel()
            {
                FirstName="",
                LastName="",
                EmailId="",
                Password="",
                PhoneNumber = ""
            };
            var response = userController.RegisterUser(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenGetAllNullUserFields_ShouldReturnBadRequestObjectResult()
        {
            UserRegisterModel data = new UserRegisterModel()
            {
                FirstName = null,
                LastName = null,
                EmailId = null,
                Password = null,
                PhoneNumber = null
            };
            var response = userController.RegisterUser(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenGetAllNullUserLoginFields_ShouldReturnBadRequestObjectResult()
        {
            UserLoginModel data = new UserLoginModel()
            {

                EmailId = null,
                Password = null
            };
            var response = userController.UserLogin(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenNullUserLoginFields_ShouldReturnBadRequestObjectResult()
        {
            UserLoginModel data = new UserLoginModel();
            data = null;
            var response = userController.UserLogin(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenGetAllEmptyUserLoginFields_ShouldReturnBadRequestObjectResult()
        {
            UserLoginModel data = new UserLoginModel()
            {

                EmailId = "",
                Password = ""
            };
            var response = userController.UserLogin(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenGetAllNullForgetPasswordFields_ShouldReturnBadRequestObjectResult()
        {

            string EmailId = null;
            var response = userController.ForgetPassword(EmailId);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenGetAllEmptyForgetPasswordFields_ShouldReturnBadRequestObjectResult()
        {

            string EmailId = "";
            var response = userController.ForgetPassword(EmailId);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenGetAllEmptyResetPasswordFields_ShouldReturnBadRequestObjectResult()
        {

            string PassWord = "";
            string ConfirmPassWord = "";
            var response = userController.ResetPassword(PassWord, ConfirmPassWord);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenGetAllNullResetPasswordFields_ShouldReturnBadRequestObjectResult()
        {

            string PassWord = null;
            string ConfirmPassWord = null;
            var response = userController.ResetPassword(PassWord, ConfirmPassWord);
            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}
