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
    public class OrderController : ControllerBase
    {

        IOrderBL orderBL;
        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("Buy")]
        public IActionResult OrderBook(int CartId)
        {
            try
            {
                var claim = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                if (CartId > 0)
                {
                 
                    var data = this.orderBL.OrderBook(claim, CartId);
                    if (data != null)
                    {

                        return this.Ok(new { status = "True", message = "Book Ordred Successfully", data });
                    }
                    else
                    {
                        return this.NotFound(new { status = "False", message = "Not Found Cart Id Or May Be Already In Use" });
                    }
                }else
                {

                    var data = this.orderBL.OrderAllBook(claim);
                    if (data != null)
                    {

                        return this.Ok(new { status = "True", message = "Book Ordred Successfully", data });
                    }
                    else
                    {
                        return this.NotFound(new { status = "False", message = "Not Found Cart Id Or May Be Already In Use" });
                    }

                }
            }
            catch (Exception exception)

            {
                return BadRequest(new { status = "False", message = exception.Message });
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
                var data = this.orderBL.GetAllOrder(claimId);
                if (data != null)
                {
                    return this.Ok(new { status = "True", message = "All Ordered", data });
                }
                else
                {
                    return this.BadRequest(new { status = "False", message = "No Order Available" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        [Route("Address")]
        public IActionResult UpdateAddress(UpdateAddressModel updateAddress)
        {
            try
            {
                //Throws Custom Exception When Fields are Null.
                if (updateAddress.PhoneNumber == null || updateAddress.Pincode == null || updateAddress.City == null || updateAddress.State == null )
                {
                    throw new Exception(OrderException.ExceptionType.NULL_EXCEPTION.ToString());
                }

                //Throws Custom Exception When Fields are Empty Strings.
                if (updateAddress.PhoneNumber == "" || updateAddress.Pincode == "" || updateAddress.City == "" || updateAddress.State == "")
                {
                    throw new Exception(OrderException.ExceptionType.EMPTY_EXCEPTION.ToString());
                }

                var claim = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                var data = this.orderBL.UpdateAddress(claim , updateAddress);
                if (data != null)
                {
                    return this.Ok(new { status = "True", message = "Book Ordred Successfully", data });
                }
                else
                {
                    return this.NotFound(new { status = "False", message = "Not Found Cart Id Or May Be Already In Use" });
                }
            }
            catch (Exception exception)

            {
                return BadRequest(new { status = "False", message = exception.Message });
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("GetAddress")]
        public IActionResult GetAddress()
        {
            try
            {
                var claim = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                var data = this.orderBL.GetAddress(claim);
                if (data != null)
                {
                    return this.Ok(new { status = "True", message = "Book Ordred Successfully", data });
                }
                else
                {
                    return this.NotFound(new { status = "False", message = "Not Found Cart Id Or May Be Already In Use" });
                }
            }
            catch (Exception exception)

            {
                return BadRequest(new { status = "False", message = exception.Message });
            }
        }
    }
}
