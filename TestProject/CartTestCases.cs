using BookStoreAPI.Controllers;
using BusinessLayer.Interface;
using BusinessLayer.Service;
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
    public class CartTestCases
    {

        CartController cartController;

        ICartBL cartBL;

        ICartRL cartRL;

        public IConfiguration configuration;

        /// <summary>
        /// initializes the fields
        /// </summary>
        public CartTestCases()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            this.configuration = configurationBuilder.Build();
            this.cartRL = new CartRL(this.configuration);
            this.cartBL = new CartBL(this.cartRL);
            this.cartController = new CartController(this.cartBL);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenAllNullCartFields_ShouldReturnBadRequestObjectResult()
        {
            int data = 0;
            var response = cartController.AddToCart(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

    }
}
