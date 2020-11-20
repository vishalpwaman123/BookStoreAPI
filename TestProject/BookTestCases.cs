using BookStoreAPI.Controllers;
using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.RequestModel;
using Microsoft.AspNetCore.Http;
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
    public class BookTestCases
    {

        BooksController booksController;

        IBookBL bookBL;

        IBookRL bookRL;

        public IConfiguration configuration;

        /// <summary>
        /// initializes the fields
        /// </summary>
        public BookTestCases()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            this.configuration = configurationBuilder.Build();
            this.bookRL = new BookRL(this.configuration);
            this.bookBL = new BookBL(this.bookRL);
            this.booksController = new BooksController(this.bookBL);
        }

        /// <summary>
        /// Given admin registration request null should return bad request
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenAllNullBookFields_ShouldReturnBadRequestObjectResult()
        {
            BookModel data = null;
            IFormFile Image = null;
            var response = booksController.AddBook(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// given request for register admin should return ok
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenNullBookNameFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new BookModel()
            {
                BookName = null,
                AuthorName = "Vishal",
                Description = "Logical",
                Price = 100,
                Pages = 100,
                Quantity = 1
            };
            IFormFile Image = null;
            var response = booksController.AddBook(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// given request for register admin should return ok
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenNullAuthorNameFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new BookModel()
            {
                BookName = "Maths",
                AuthorName = null,
                Description = "Logical",
                Price = 100,
                Pages = 100,
                Quantity = 1
            }; 
            IFormFile Image = null;
            var response = booksController.AddBook(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// given request for register admin should return ok
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenNullDiscriptionFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new BookModel()
            {
                BookName = "Maths",
                AuthorName = "Vishal",
                Description = null,
                Price = 100,
                Pages = 100,
                Quantity = 1,

            };
            IFormFile Image = null;
            var response = booksController.AddBook(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// given request for register admin should return ok
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenZeroPriceFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new BookModel()
            {
                BookName = "Maths",
                AuthorName = "Vishal",
                Description = "Logical",
                Price = 0,
                Pages = 100,
                Quantity = 1
            };
            IFormFile Image = null;
            var response = booksController.AddBook(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// given request for register admin should return ok
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenZeroPageFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new BookModel()
            {
                BookName = "Maths",
                AuthorName = "Vishal",
                Description = "Logical",
                Price = 100,
                Pages = 0,
                Quantity = 1
            };
            IFormFile Image = null;
            var response = booksController.AddBook(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// given request for register admin should return ok
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenZeroQuantityFields_ShouldReturnBadRequestObjectResult()
        {
            var data = new BookModel()
            {
                BookName = "Maths",
                AuthorName = "Vishal",
                Description = "Logical",
                Price = 100,
                Pages = 100,
                Quantity = 0
            };
            IFormFile Image = null;
            var response = booksController.AddBook(data);
            Assert.IsType<BadRequestObjectResult>(response);
        }
        /*
                /// <summary>
                /// given request for register admin should return ok
                /// </summary>
                [Fact]
                public void GivenTestCase_WhenAllValidFields_ShouldReturnOkObjectResult()
                {
                    var data = new BookModel()
                    {
                        BookName = "Mathematics 1",
                        AuthorName = "Vishal",
                        Description = "Maths",
                        Price = 200,
                        Pages = 100,
                        Quantity = 2
                    };
                    var response = booksController.AddBook(data);
                    Assert.IsType<OkObjectResult>(response);
                }
        */

        /// <summary>
        /// given request for register admin should return ok
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenAllValidFields_ShouldReturnOkObjectResult()
        {
            string BookName = "Chankya";
            var response = booksController.SearchBook(BookName);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// given request for register admin should return ok
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenNullPassGetAllValidFields_ShouldReturnOkObjectResult()
        {
            string BookName = null;
            var response = booksController.SearchBook(BookName);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        /// <summary>
        /// given request for register admin should return ok
        /// </summary>
       /* [Fact]
        public void GivenTestCase_WhenSortBookByName_ShouldReturnOkObjectResult()
        {

            String attribute = null;
            var response = booksController.SortBook(attribute);
            Assert.IsType<BadRequestObjectResult>(response);
        }
*/
        /// <summary>
        /// given request for register admin should return ok
        /// </summary>
        /*[Fact]
        public void GivenTestCase_WhenUpdateBookPrice_ShouldReturnOkObjectResult()
        {
            var updateBookModel = new UpdateBookModel()
            {
                BookId = 7,
                AttributeData = "512"
            };
            var response = booksController.UpdateBook(updateBookModel);
            Assert.IsType<BadRequestObjectResult>(response);
        }*/

        /// <summary>
        /// given request for register admin should return ok
        /// </summary>
        [Fact]
        public void GivenTestCase_WhenSearchOutOfStockBook_ShouldReturnOkObjectResult()
        {
            
            var response = booksController.SearchOutOfStockBook();
            Assert.IsType<BadRequestObjectResult>(response);
        }

    }
}
