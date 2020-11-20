using BusinessLayer.Interface;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class OrderBL : IOrderBL
    {

        /// <summary>
        /// RL Reference.
        /// </summary>
        private IOrderRL orderRL;

        /// <summary>
        /// Constructor For Setting orderRL Instance.
        /// </summary>
        /// <param name="userRL"></param>
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }

        public ROrderModel OrderBook(int claimID, int CartID)
        {
            try
            {
                return this.orderRL.OrderBook(claimID, CartID);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<RAllOrderModel> OrderAllBook(int claimID)
        {
            try
            {
                return this.orderRL.OrderAllBook(claimID);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ROrderDetailModel> GetAllOrder(int claimID)
        {
            try
            {
                return this.orderRL.GetAllOrder(claimID);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public RUpdateAddressModel UpdateAddress(int claimID, UpdateAddressModel updateAddressModel)
        {
            try
            {
                return this.orderRL.UpdateAddress(claimID, updateAddressModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public RUpdateAddressModel GetAddress(int claimID)
        {
            try
            {
                return this.orderRL.GetAddress(claimID);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
