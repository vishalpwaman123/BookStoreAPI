using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CloudinaryDotNet;
using CommonLayer.Exceptions;
using CommonLayer.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CommonLayer.Enum;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        IBookBL bookBL;
        public BooksController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("")]
        public IActionResult AddBook([FromForm] BookModel bookModel)
        {
            try
            {
                
                int claimId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                if (bookModel != null)
                {
                    if (bookModel.AuthorName == null || bookModel.BookName == null || bookModel.Description == null)
                    {
                        throw new Exception(BookException.ExceptionType.NULL_EXCEPTION.ToString());
                    }

                    //Throws Custom Exception When Fields are Empty Strings.
                    if (bookModel.AuthorName == "" || bookModel.BookName == "" || bookModel.Description == "")
                    {
                        throw new Exception(BookException.ExceptionType.EMPTY_EXCEPTION.ToString());
                    }

                    if (bookModel.Pages == 0)
                    {
                        throw new Exception(BookException.ExceptionType.INVALID_PAGE_COUNT.ToString());
                    }

                    if (bookModel.Price == 0)
                    {
                        throw new Exception(BookException.ExceptionType.INVALID_PRICE.ToString());
                    }

                    if (bookModel.Quantity == 0)
                    {
                        throw new Exception(BookException.ExceptionType.INVALID_QUANTITY.ToString());
                    }

                    var data = this.bookBL.AddBook(bookModel, claimId);
                    if (data != null)
                    {
                        return this.Ok(new { status = "True", message = "Book Added Successfully", data });
                    }
                    else
                    {
                        return this.Conflict(new { status = "False", message = "Failed To Add Book" });
                    }
                }
                else
                {
                    return this.BadRequest(new { status = "False", message = "Failed To Add Book" });
                }
            }

            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        /*[Authorize(Roles = "admin")]
        [HttpPost]
        [Route("Image")]
        public IActionResult AddImage(int BookID, IFormFile Image)
        {
            try
            {
                
                int claimId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                if (BookID > 0 )
                {
                    if (Image == null)
                    {
                        throw new Exception(BookException.ExceptionType.NULL_EXCEPTION.ToString());
                    }

                    var data = this.bookBL.AddImage(claimId, BookID, Image);
                    if (data != null)
                    {
                        return this.Ok(new { status = "True", message = "Image Added Successfully", data });
                    }
                    else
                    {
                        return this.Conflict(new { status = "False", message = "Failed To Add Image" });
                    }
                }
                else
                {
                    return this.BadRequest(new { status = "False", message = "Failed To Add Book" });
                }
            }

            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }*/

        /*[Authorize(Roles = "admin")]
        [HttpPut]
        [Route("Image")]
        public IActionResult UpdateImage(int BookID, IFormFile Image)
        {
            try
            {

                int claimId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                if (BookID > 0)
                {
                    if (Image == null)
                    {
                        throw new Exception(BookException.ExceptionType.NULL_EXCEPTION.ToString());
                    }

                    var data = this.bookBL.AddImage(claimId, BookID, Image);
                    if (data != null)
                    {
                        return this.Ok(new { status = "True", message = "Image Update Successfully", data });
                    }
                    else
                    {
                        return this.Conflict(new { status = "False", message = "Failed To Update Image" });
                    }
                }
                else
                {
                    return this.BadRequest(new { status = "False", message = "Failed To Add Book" });
                }
            }

            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }*/

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public IActionResult SearchBook([FromQuery] string Bookname)
        {
            try
            {
                if (Bookname != null)
                {

                    var data = this.bookBL.SearchBook(Bookname);
                    if (data != null)
                    {
                        return this.Ok(new { status = "True", message = "Search Books", data });
                    }
                    else
                    {
                        return this.NotFound(new { status = "False", message = "Books Not Available" });
                    }
                }
                else
                {
                    var data = this.bookBL.GetAllBooks();
                    if (data != null)
                    {
                        return this.Ok(new { status = "True", message = "All Books", data });
                    }
                    else
                    {
                        return this.NotFound(new { status = "False", message = "Books Not Available" });
                    }
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpGet]
        [Route("FilterByPrice")]
        [AllowAnonymous]
        public IActionResult BookFilter(int FirstPrice , int FinalPrice)
        {
            try
            {
                if (FirstPrice != 0 && FinalPrice !=0)
                {

                    var data = this.bookBL.BookFilter(FirstPrice, FinalPrice);
                    if (data != null)
                    {
                        return this.Ok(new { status = "True", message = "Search Books", data });
                    }
                    else
                    {
                        return this.NotFound(new { status = "False", message = "Books Not Available" });
                    }
                }
                else
                {
                    var data = this.bookBL.GetAllBooks();
                    if (data != null)
                    {
                        return this.Ok(new { status = "True", message = "All Books", data });
                    }
                    else
                    {
                        return this.NotFound(new { status = "False", message = "Books Not Available" });
                    }
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [HttpGet]
        [Route("Sort")]
        [AllowAnonymous]
        public IActionResult SortBook(String attribute)
        {
            try
            {
                if (attribute != null)
                {
                    int flag = -1;
                    if ((attribute.Equals(Attributes.AuthorName.ToString())) || (attribute.Equals(Attributes.Authorname.ToString())) ||
                        (attribute.Equals(attributes.authorname.ToString())))
                    {
                        flag = 0;
                    }
                    else
                    if ((attribute.Equals(Attributes.BookName.ToString())) || (attribute.Equals(Attributes.Bookname.ToString())) ||
                        (attribute.Equals(attributes.bookname.ToString())))
                    {
                        flag = 1;
                    }
                    else
                    if ((attribute.Equals(Attributes.Description.ToString())) || (attribute.Equals(attributes.authorname.ToString())))
                    {
                        flag = 2;
                    }
                    else
                    if ((attribute.Equals(Attributes.Pages.ToString())) || (attribute.Equals(attributes.pages.ToString())))
                    {
                        flag = 3;
                    }
                    else
                    if ((attribute.Equals(Attributes.Price.ToString())) || (attribute.Equals(attributes.price.ToString())))
                    {
                        flag = 4;
                    }
                    else
                    if ((attribute.Equals(Attributes.Quantity.ToString())) || (attribute.Equals(attributes.quantity.ToString())))
                    {
                        flag = 5;
                    }

                    if (flag != -1)
                    {
                        var data = this.bookBL.SortBook(flag);
                        if (data != null)
                        {
                            return this.Ok(new { status = "True", message = "Sort Books by Name Sucessfully", data });
                        }
                        else
                        {
                            return this.NotFound(new { status = "False", message = "Sorting Operation UnSucessfully" });
                        }
                    }
                }

                throw new Exception(BookException.ExceptionType.INVALID_ATTRIBUTE.ToString() + " eg. BookName , AuthorName , Description , Price , Pages , Quantity");
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("")]
        public IActionResult UpdateBook([FromForm] UpdateBookModel updateBookModel)
        {
            try
            {
                //AttributesNameExceptionHandler(updateBookModel);

                if (updateBookModel.BookId > 0)
                {
                    //int flag = -1;
                    /*if ((updateBookModel.AttributeName.Equals(Attributes.AuthorName.ToString())) || (updateBookModel.AttributeName.Equals(Attributes.Authorname.ToString())) ||
                        (updateBookModel.AttributeName.Equals(attributes.authorname.ToString())))
                    {
                        flag = 0;
                        ExceptionHandler(updateBookModel,flag);
                    }
                    else
                    if ((updateBookModel.AttributeName.Equals(Attributes.BookName.ToString())) || (updateBookModel.AttributeName.Equals(Attributes.Bookname.ToString())) ||
                        (updateBookModel.AttributeName.Equals(attributes.bookname.ToString())))
                    {
                        flag = 1;
                        ExceptionHandler(updateBookModel, flag);
                    }
                    else
                    if ((updateBookModel.AttributeName.Equals(Attributes.Description.ToString())) || (updateBookModel.AttributeName.Equals(attributes.authorname.ToString())))
                    {
                        flag = 2;
                        ExceptionHandler(updateBookModel, flag);
                    }
                    else
                    if ((updateBookModel.AttributeName.Equals(Attributes.Pages.ToString())) || (updateBookModel.AttributeName.Equals(attributes.pages.ToString())))
                    {
                        flag = 3;
                        ExceptionHandler(updateBookModel, flag);
                    }
                    else
                    if ((updateBookModel.AttributeName.Equals(Attributes.Price.ToString())) || (updateBookModel.AttributeName.Equals(attributes.price.ToString())))
                    {
                        flag = 4;
                        ExceptionHandler(updateBookModel, flag);
                    }
                    else
                    if ((updateBookModel.AttributeName.Equals(Attributes.Quantity.ToString())) || (updateBookModel.AttributeName.Equals(attributes.quantity.ToString())))
                    {
                        flag = 5;
                        ExceptionHandler(updateBookModel, flag);
                    }
*/
                        if (null == (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value))
                        {
                            throw new Exception(BookException.ExceptionType.NULL_EXCEPTION.ToString());
                        }

                        int claimId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                        var data = this.bookBL.UpdateBook(updateBookModel, claimId);
                        if (data != null)
                        {
                            return this.Ok(new { status = "True", message = "Book Updated Successfully", data });
                        }
                        else
                        {
                            return this.NotFound(new { status = "False", message = "Failed To Update Book" });
                        }
                    
                }
                else
                {
                    throw new Exception(BookException.ExceptionType.INVALID_BOOKID.ToString());
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        [Route("{bookId}")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                if (bookId > 0)
                {
                    int claimId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value);

                    var data = this.bookBL.DeleteBook(bookId, claimId);
                    if (data != null)
                    {
                        return this.Ok(new { status = "True", message = "Book Deleted Successfully" , data });
                    }
                    else
                    {
                        return this.BadRequest(new { status = "False", message = "Failed To Delete Book" });
                    }
                }
                else
                {
                    throw new Exception(BookException.ExceptionType.INVALID_BOOKID.ToString());
                }

            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }


        [HttpGet]
        [Route("OutOfStock")]
        [AllowAnonymous]
        public IActionResult SearchOutOfStockBook()
        {
            try
            {
                
                    var data = this.bookBL.SearchOutOfStockBook();
                    if (data != null)
                    {
                        return this.Ok(new { status = "True", message = "Search Out Of Stock Books", data });
                    }
                    else
                    {
                        return this.BadRequest(new { status = "False", message = "No Out Of Books Available" });
                    }
            
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        /*void AttributesNameExceptionHandler(UpdateBookModel updateBookModel)
        {
            try
            {
                if ((updateBookModel.AttributeName.Equals(Attributes.AuthorName.ToString())) || (updateBookModel.AttributeName.Equals(Attributes.Authorname.ToString())) ||
                            (updateBookModel.AttributeName.Equals(attributes.authorname.ToString())) ||
                            (updateBookModel.AttributeName.Equals(Attributes.BookName.ToString())) || (updateBookModel.AttributeName.Equals(Attributes.Bookname.ToString())) ||
                            (updateBookModel.AttributeName.Equals(attributes.bookname.ToString())) ||
                            (updateBookModel.AttributeName.Equals(Attributes.Description.ToString())) || (updateBookModel.AttributeName.Equals(attributes.authorname.ToString())) ||
                            (updateBookModel.AttributeName.Equals(Attributes.Price.ToString())) || (updateBookModel.AttributeName.Equals(attributes.price.ToString())) ||
                            (updateBookModel.AttributeName.Equals(Attributes.Price.ToString())) || (updateBookModel.AttributeName.Equals(attributes.price.ToString())) ||
                            (updateBookModel.AttributeName.Equals(Attributes.Quantity.ToString())) || (updateBookModel.AttributeName.Equals(attributes.quantity.ToString())))
                {
                    return;
                }else
                {
                    throw new Exception(BookException.ExceptionType.INVALID_ATTRIBUTE.ToString() + " eg. BookName , AuthorName , Description , Price , Pages , Quantity");

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        void ExceptionHandler(UpdateBookModel updateBookModel, int flag)
        {
            try
            {
                int AttributeData = 0;
                if (flag == 0 || flag == 1 || flag == 2)
                {
                    if (updateBookModel.AttributeData == null)
                    {
                        throw new Exception(updateBookModel.AttributeName + BookException.ExceptionType.NULL_EXCEPTION.ToString());
                    }

                    if (updateBookModel.AttributeData == "")
                    {
                        throw new Exception(updateBookModel.AttributeName + BookException.ExceptionType.EMPTY_EXCEPTION.ToString());
                    }
                }
                else if (flag == 3)
                {
                    AttributeData = Convert.ToInt32(updateBookModel.AttributeData);
                    if (AttributeData < 1)
                    {
                        throw new Exception(updateBookModel.AttributeName + " " + BookException.ExceptionType.INVALID_PAGE_COUNT.ToString());
                    }

                }
                else if (flag == 4)
                {
                    AttributeData = Convert.ToInt32(updateBookModel.AttributeData);
                    if (AttributeData < 1)
                    {
                        throw new Exception(updateBookModel.AttributeName + " " + BookException.ExceptionType.INVALID_PRICE_DATA.ToString());
                    }

                }
                else
                {
                    AttributeData = Convert.ToInt32(updateBookModel.AttributeData);
                    if (AttributeData < 1)
                    {
                        throw new Exception(updateBookModel.AttributeName + " " + BookException.ExceptionType.INVALID_QUANTITY_DATA.ToString());
                    }

                }
            }catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }*/
    }
}
