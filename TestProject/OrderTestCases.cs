using BookStoreAPI.Controllers;
using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.RequestModel;
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
    public class OrderTestCases
    {
        OrderController orderController;

        IOrderBL orderBL;

        IOrderRL orderRL;

        public IConfiguration configuration;

        /// <summary>
        /// initializes the fields
        /// </summary>
        public OrderTestCases()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            this.configuration = configurationBuilder.Build();
            this.orderRL = new OrderRL(this.configuration);
            this.orderBL = new OrderBL(this.orderRL);
            this.orderController = new OrderController(this.orderBL);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenAllNullCartFields_ShouldReturnBadRequestObjectResult()
        {
            int data = 0;
            var response = orderController.OrderBook(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenGetAllEmptyOrderFields_ShouldReturnBadRequestObjectResult()
        {
            UpdateAddressModel data = new UpdateAddressModel()
            {
                Locality = "",
                City = "",
                State = "",
                PhoneNumber = "",
                Pincode = "",
                LandMark = ""
            };
            var response = orderController.UpdateAddress(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenGetAllNullOrderFields_ShouldReturnBadRequestObjectResult()
        {
            UpdateAddressModel data = new UpdateAddressModel()
            {
                Locality = null,
                City = null,
                State = null,
                PhoneNumber = null,
                Pincode = null,
                LandMark = null
            };
            var response = orderController.UpdateAddress(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenGetLocalityNullOrderFields_ShouldReturnBadRequestObjectResult()
        {
            UpdateAddressModel data = new UpdateAddressModel()
            {
                Locality = null,
                City = "Pune",
                State = "Maharashtra",
                PhoneNumber = "7758039722",
                Pincode = "411048",
                LandMark = "Temple"
            };
            var response = orderController.UpdateAddress(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenCityNullOrderFields_ShouldReturnBadRequestObjectResult()
        {
            UpdateAddressModel data = new UpdateAddressModel()
            {
                Locality = "Kondhwa",
                City = null,
                State = "Maharashtra",
                PhoneNumber = "7758039722",
                Pincode = "411048",
                LandMark = "Temple"
            };
            var response = orderController.UpdateAddress(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenStateNullOrderFields_ShouldReturnBadRequestObjectResult()
        {
            UpdateAddressModel data = new UpdateAddressModel()
            {
                Locality = "Kondhwa",
                City = "Pune",
                State = null,
                PhoneNumber = "7758039722",
                Pincode = "411048",
                LandMark = "Temple"
            };
            var response = orderController.UpdateAddress(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenGetPhoneNumberNullOrderFields_ShouldReturnBadRequestObjectResult()
        {
            UpdateAddressModel data = new UpdateAddressModel()
            {
                Locality = "Kondhwa",
                City = "Pune",
                State = "Maharashtra",
                PhoneNumber = null,
                Pincode = "411048",
                LandMark = "Temple"
            };
            var response = orderController.UpdateAddress(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenGetPincodeNullOrderFields_ShouldReturnBadRequestObjectResult()
        {
            UpdateAddressModel data = new UpdateAddressModel()
            {
                Locality = "Kondhwa",
                City = "Pune",
                State = "Maharashtra",
                PhoneNumber = "7758039722",
                Pincode = null,
                LandMark = "Temple"
            };
            var response = orderController.UpdateAddress(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenGetLandMarkNullOrderFields_ShouldReturnBadRequestObjectResult()
        {
            UpdateAddressModel data = new UpdateAddressModel()
            {
                Locality = "Kondhwa",
                City = "Pune",
                State = "Maharashtra",
                PhoneNumber = "7758039722",
                Pincode = "411048",
                LandMark = null
            };
            var response = orderController.UpdateAddress(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenPhoneNumberEmptyOrderFields_ShouldReturnBadRequestObjectResult()
        {
            UpdateAddressModel data = new UpdateAddressModel()
            {
                Locality = "",
                City = "Pune",
                State = "Maharashtra",
                PhoneNumber = "7758039722",
                Pincode = "411048",
                LandMark = "Temple"
            };
            var response = orderController.UpdateAddress(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenCityEmptyOrderFields_ShouldReturnBadRequestObjectResult()
        {
            UpdateAddressModel data = new UpdateAddressModel()
            {
                Locality = "Kondhwa",
                City = "",
                State = "Maharashtra",
                PhoneNumber = "7758039722",
                Pincode = "411048",
                LandMark = "Temple"
            };
            var response = orderController.UpdateAddress(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenStateEmptyOrderFields_ShouldReturnBadRequestObjectResult()
        {
            UpdateAddressModel data = new UpdateAddressModel()
            {
                Locality = "Kondhwa",
                City = "Pune",
                State = "",
                PhoneNumber = "7758039722",
                Pincode = "411048",
                LandMark = "Temple"
            };
            var response = orderController.UpdateAddress(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenGetPhoneNumberEmptyOrderFields_ShouldReturnBadRequestObjectResult()
        {
            UpdateAddressModel data = new UpdateAddressModel()
            {
                Locality = "Kondhwa",
                City = "Pune",
                State = "Maharashtra",
                PhoneNumber = "",
                Pincode = "411048",
                LandMark = "Temple"
            };
            var response = orderController.UpdateAddress(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenPincodeEmptyOrderFields_ShouldReturnBadRequestObjectResult()
        {
            UpdateAddressModel data = new UpdateAddressModel()
            {
                Locality = "Kondhwa",
                City = "Pune",
                State = "Maharashtra",
                PhoneNumber = "7758039722",
                Pincode = "",
                LandMark = "Temple"
            };
            var response = orderController.UpdateAddress(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenLandMarkEmptyOrderFields_ShouldReturnBadRequestObjectResult()
        {
            UpdateAddressModel data = new UpdateAddressModel()
            {
                Locality = "Kondhwa",
                City = "Pune",
                State = "Maharashtra",
                PhoneNumber = "7758039722",
                Pincode = "411048",
                LandMark = ""
            };
            var response = orderController.UpdateAddress(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}
