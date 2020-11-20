using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Exceptions;
using CommonLayer.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {

        IWishListBL wishListBL;
        public WishListController(IWishListBL wishListBL)
        {
            this.wishListBL = wishListBL;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("")]
        public IActionResult AddWishList(int BookId)
        {
            try
            {
                if(BookId < 0)
                {
                    throw new Exception(WishListException.ExceptionType.INVALID_BOOKID.ToString());

                }

                int claimId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                var data = this.wishListBL.AddBookToWishList(claimId, BookId);
                if (data != null)
                {
                    return this.Ok(new { status = "True", message = "Book Added To WishList Successfully", data });
                }
                else
                {
                    return this.BadRequest(new { status = "False", message = "Failed To Add WishList" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("")]
        public IActionResult GetWishList()
        {
            try
            {
                int claimId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                var data = this.wishListBL.GetAllWishList(claimId);
                if (data != null)
                {
                    return this.Ok(new { status = "True", message = "All Wish List", data });
                }
                else
                {
                    return this.BadRequest(new { status = "False", message = "WishLists Not Available" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        [Route("{wishListId}")]
        public IActionResult DeleteWishList(int wishListId)
        {
            try
            {
                if (wishListId < 0)
                {
                    throw new Exception(WishListException.ExceptionType.INVALID_WISHLISTID.ToString());
                }
                int claim = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                var data = this.wishListBL.DeleteWishList(claim, wishListId);
                if (data != null)
                {
                    return this.Ok(new { status = "True", message = "Wish List Delete Successfully" , data });
                }
                else
                {
                    return this.BadRequest(new { status = "False", message = "Failed To Delete Wish List" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("MoveToCart")]
        public IActionResult WishListMoveToCart(int wishListId)
        {
            try
            {
                if (wishListId < 0)
                {
                    throw new Exception(WishListException.ExceptionType.INVALID_WISHLISTID.ToString());
                }
                int claimId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                var data = this.wishListBL.WishListMoveToCart(claimId, wishListId);
                if (data != null)
                {
                    return this.Ok(new { status = "True", message = "Book Added WishList To Cart Successfully", data });
                }
                else
                {
                    return this.BadRequest(new { status = "False", message = "Failed To Added WishList To Cart Successfully" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

    }
}
