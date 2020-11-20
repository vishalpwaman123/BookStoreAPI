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
using Xunit;

namespace TestProject
{
    public class AdminTestCase
    {
        UserManager<IdentityUser> userManager;

        AdminController adminController;

        IAdminBL adminBL;

        IAdminRL adminRL;

        public IConfiguration configuration;

        /// <summary>
        /// initializes the fields
        /// </summary>
       /* public AdminTestCase()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            this.configuration = configurationBuilder.Build();
            this.adminRL = new AdminRL(this.configuration, userManager);
            this.adminBL = new AdminBL(this.adminRL);
            this.adminController = new AdminController(this.adminBL, configuration);
        }*/

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenAllNullAdminFields_ShouldReturnBadRequestObjectResult()
        {
            AdminRegisterModel data = null;
            var response = adminController.RegisterAdmin(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// given request for register admin should return ok
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenAllNullAdminNameFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new AdminRegisterModel()
            {
                AdminName = null,
                AdminEmailId = "vishal@gmail.com",
                Gender = "Male",
                Password = "123456",
            };
            var response = adminController.RegisterAdmin(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// given request for register admin should return ok
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenAllNullAdminEmailFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new AdminRegisterModel()
            {
                AdminName = "Vishal",
                AdminEmailId = null,
                Gender = "Male",
                Password = "123456",
            };
            var response = adminController.RegisterAdmin(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// given request for register admin should return ok
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenAllNullGenderFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new AdminRegisterModel()
            {
                AdminName = "Vishal",
                AdminEmailId = "vishal@gmail.com",
                Gender = null,
                Password = "123456",
            };
            var response = adminController.RegisterAdmin(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// given request for register admin should return ok
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenAllNullAdminPasswordFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new AdminRegisterModel()
            {
                AdminName = "Vishal",
                AdminEmailId = "vishal@gmail.com",
                Gender = "Male",
                Password = null,
            };
            var response = adminController.RegisterAdmin(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// given request for register admin should return ok
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenAllEmptyAdminAllFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new AdminRegisterModel()
            {
                AdminName = "",
                AdminEmailId = "",
                Gender = "",
                Password = "",
            };
            var response = adminController.RegisterAdmin(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// given request for register admin should return ok
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenAllEmptyAdminNameFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new AdminRegisterModel()
            {
                AdminName = "",
                AdminEmailId = "vishal@gmail.com",
                Gender = "Male",
                Password = "12345",
            };
            var response = adminController.RegisterAdmin(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// given request for register admin should return ok
        /// </summary>
        
        [Fact]
        public void GivenTestCase_WhenEmptyAdminEmailIdFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new AdminRegisterModel()
            {
                AdminName = "vishal",
                AdminEmailId = "",
                Gender = "Male",
                Password = "12345",
            };
            var response = adminController.RegisterAdmin(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// given request for register admin should return ok
        /// </summary>

        [Fact]
        public void GivenTestCase_WhenEmptyAdminGenderFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new AdminRegisterModel()
            {
                AdminName = "vishal",
                AdminEmailId = "vishal@gmail.com",
                Gender = "",
                Password = "12345",
            };
            var response = adminController.RegisterAdmin(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// given request for register admin should return ok
        /// </summary>

        [Fact]
        public void GivenTestCase_WhenEmptyAdminPasswordFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new AdminRegisterModel()
            {
                AdminName = "vishal",
                AdminEmailId = "vishal@gmail.com",
                Gender = "Male",
                Password = "",
            };
            var response = adminController.RegisterAdmin(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public void GivenTestCase_WhenEmptyAdminRegistrationFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new AdminRegisterModel()
            {
                AdminName = "vishal",
                AdminEmailId = "vishal@gmail.com",
                Gender = "Male",
                Password = "vishal",
            };
            var response = adminController.RegisterAdmin(data);
            Assert.IsType<OkObjectResult>(response);
        }

       
        /// given request for register admin should return ok
        /// </summary>

        /*[Fact]
        public void GivenTestCase_WhenEmptyAdminEmailLoginFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new AdminLoginModel()
            {

                Email = "",
                Password = "mahesh"

            };
            var response = adminController.AdminLogin(data) as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(response);
        }*/

        /// given request for register admin should return ok
        /// </summary>
/*
        [Fact]
        public void GivenTestCase_WhenEmptyAdminPasswordLoginFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new AdminLoginModel()
            {

                Email = "mahesh@gmail.com",
                Password = ""

            };
            var response = adminController.AdminLogin(data) as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// given request for register admin should return ok
        /// </summary>

        [Fact]
        public void GivenTestCase_WhenNullAdminEmailLoginFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new AdminLoginModel()
            {

                Email = null,
                Password = "mahesh"

            };
            var response = adminController.AdminLogin(data) as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// given request for register admin should return ok
        /// </summary>

        [Fact]
        public void GivenTestCase_WhenNullAdminPasswordLoginFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new AdminLoginModel()
            {

                Email = "mahesh@gmail.com",
                Password = null

            };
            var response = adminController.AdminLogin(data) as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(response);
        }*/
    }
}
