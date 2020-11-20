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
    public class WishListTestCase
    {

        WishListController wishListController;

        IWishListBL wishListBL;

        IWishListRL wishListRL;

        public IConfiguration configuration;

        /// <summary>
        /// initializes the fields
        /// </summary>
        public WishListTestCase()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            this.configuration = configurationBuilder.Build();
            this.wishListRL = new WishListRL(this.configuration);
            this.wishListBL = new WishListBL(this.wishListRL);
            this.wishListController = new WishListController(this.wishListBL);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenAddWishListZeroFields_ShouldReturnBadRequestObjectResult()
        {
            int data =0;
            var response = wishListController.AddWishList(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenDeleteWishListZeroFields_ShouldReturnBadRequestObjectResult()
        {
            int data = 0;
            var response = wishListController.DeleteWishList(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenWishListMoveToCartZeroFields_ShouldReturnBadRequestObjectResult()
        {
            int data = 0;
            var response = wishListController.WishListMoveToCart(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

    }
}
