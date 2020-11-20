using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("")]
        public IActionResult AddToCart(int BookId)
        {
            try
            {
                if (BookId <= 0)
                {
                    throw new Exception(WishListException.ExceptionType.INVALID_BOOKID.ToString());

                }

                int claimId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                var data = this.cartBL.AddToCart(claimId, BookId);
                if (data != null)
                {
                    return this.Ok(new { status = "True", message = "Book Added To Cart Successfully", data });
                }
                else
                {
                    return this.BadRequest(new { status = "False", message = "Failed To Add Cart" });
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
        public IActionResult GetAllCart()
        {
            try
            {
                int claimId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                var data = this.cartBL.GetAllCart(claimId);
                if (data != null)
                {
                    return this.Ok(new { status = "True", message = "All Carts", data });
                }
                else
                {
                    return this.BadRequest(new { status = "False", message = "Carts Not available" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }


        [Authorize(Roles = "User")]
        [HttpDelete]
        [Route("{cartId}")]
        public IActionResult DeleteCart(int cartId)
        {
            try
            {
                if (cartId > 0)
                {
                    var claim = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                    var data = this.cartBL.DeleteCart(claim, cartId);
                    if (data != null)
                    {
                        return this.Ok(new { status = "True", message = "Cart Deleted Successfully", data });
                    }
                    else
                    {
                        return this.NotFound(new { status = "False", message = "Failed To Delete Carts" });
                    }
                }
                else
                {
                    throw new Exception(CartException.ExceptionType.INVALID_CARTID.ToString());
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

    }
}
