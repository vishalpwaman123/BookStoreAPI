using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IOrderBL
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="claimID"></param>
        /// <param name="CartID"></param>
        /// <returns></returns>
        ROrderModel OrderBook(int claimID, int CartID);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimID"></param>
        /// <param name="CartID"></param>
        /// <returns></returns>
        List<RAllOrderModel> OrderAllBook(int claimID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimID"></param>
        /// <returns></returns>
        List<ROrderDetailModel> GetAllOrder(int claimID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimID"></param>
        /// <returns></returns>
        RUpdateAddressModel UpdateAddress(int claimID, UpdateAddressModel updateAddressModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimID"></param>
        /// <returns></returns>
        RUpdateAddressModel GetAddress(int claimID);


        
    }
}
