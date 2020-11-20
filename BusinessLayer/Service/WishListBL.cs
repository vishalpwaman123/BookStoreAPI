using BusinessLayer.Interface;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class WishListBL : IWishListBL
    {

        /// <summary>
        /// RL Reference.
        /// </summary>
        private IWishListRL WishListRL;

        /// <summary>
        /// Constructor For Setting UserRL Instance.
        /// </summary>
        /// <param name="userRL"></param>
        public WishListBL(IWishListRL WishListRL)
        {
            this.WishListRL = WishListRL;
        }


        /// <summary>
        /// Function For Register User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public RAddWishListModel AddBookToWishList(int userId, int BookId)
        {
            try
            {
                return this.WishListRL.AddBookToWishList(userId, BookId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }


        /// <summary>
        /// Function For Get User Wish List.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<RAddWishListModel> GetAllWishList(int claimid)
        {
            try
            {
                return this.WishListRL.GetAllWishList(claimid);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Function For Get User Wish List.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public RAddWishListModel DeleteWishList(int claimid, int wishListId)
        {
            try
            {
                return this.WishListRL.DeleteWishList(claimid, wishListId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Function For Get User Wish List.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public RAddWishListModel WishListMoveToCart(int claimid, int wishListId)
        {
            try
            {
                return this.WishListRL.WishListMoveToCart(claimid, wishListId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

    }
}
