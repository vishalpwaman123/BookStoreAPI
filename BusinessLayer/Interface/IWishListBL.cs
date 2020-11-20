using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishListBL
    {
        /// <summary>
        /// Abstact Function For Register User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        RAddWishListModel AddBookToWishList(int userId, int BookId);

        /// <summary>
        /// Abstract function for Get all wish list  
        /// </summary>
        /// <param name="claimid"></param>
        /// <returns></returns>
        List<RAddWishListModel> GetAllWishList(int claimid);

        /// <summary>
        /// Abstract function for Delete User wish list  
        /// </summary>
        /// <param name="claimid"></param>
        /// <returns></returns>
        RAddWishListModel DeleteWishList(int claimid,int wishListId);

        /// <summary>
        /// Abstract function for Move User wish list To cart
        /// </summary>
        /// <param name="claimid"></param>
        /// <returns></returns>
        RAddWishListModel WishListMoveToCart(int claimid, int wishListId);


    }
}
