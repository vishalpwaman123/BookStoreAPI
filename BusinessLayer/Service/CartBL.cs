using BusinessLayer.Interface;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CartBL : ICartBL
    {

        /// <summary>
        /// RL Reference.
        /// </summary>
        private ICartRL cartRL;

        /// <summary>
        /// Constructor For Setting CartRLRL Instance.
        /// </summary>
        /// <param name="userRL"></param>
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        /// <summary>
        /// Function For Register User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public RAllCartDetail AddToCart(int userId, int CartId)
        {
            try
            {
                return this.cartRL.AddToCart(userId, CartId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Function For Register User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<RAllCartDetail> GetAllCart(int userId)
        {
            try
            {
                return this.cartRL.GetAllCart(userId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Function For Register User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public RAllCartDetail DeleteCart(int userId, int CartId)
        {
            try
            {
                return this.cartRL.DeleteCart(userId, CartId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

    }
}
