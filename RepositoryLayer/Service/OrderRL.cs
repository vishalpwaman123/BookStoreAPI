using CommonLayer.Exceptions;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class OrderRL : IOrderRL
    {

        /// <summary>
        /// create field for configuration 
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initialzes the memory and inject the configuration interface
        /// </summary>
        /// <param name="configuration"></param>
        public OrderRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public ROrderModel OrderBook(int claimID, int CartID)
        {
            try
            {
                int status;
                /*DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                List<StoredProcedureParameter> paramList = new List<StoredProcedureParameter>();
                
                paramList.Add(new StoredProcedureParameter("@CartID", CartID));
                paramList.Add(new StoredProcedureParameter("@claimID", claimID));
                
                DataTable table = databaseConnection.StoredProcedureExecuteReader("spOrderBook", paramList);
                var OrderData = new ROrderModel();

                foreach (DataRow dataRow in table.Rows)
                {
                    status = sqlDataReader.GetInt32(0);
                    if(status == 1)
                    OrderData = new ROrderModel();
                    OrderData.CartID = Convert.ToInt32(dataRow["CartID"]);
                    OrderData.BookID = Convert.ToInt32(dataRow["BookID"]);
                    OrderData.UserID = Convert.ToInt32(dataRow["UserID"]);
                    OrderData.AddressID = Convert.ToInt32(dataRow["AddressID"]);
                    OrderData.Locality = dataRow["Locality"].ToString();
                    OrderData.City = dataRow["City"].ToString();
                    OrderData.State = dataRow["State"].ToString();
                    OrderData.PhoneNumber = dataRow["PhoneNumber"].ToString();
                    OrderData.Pincode = dataRow["Pincode"].ToString();
                    OrderData.LandMark = dataRow["LandMark"].ToString();
                    OrderData.CreatedDate = dataRow["ModificationDate"].ToString();
                }*/

                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                SqlConnection sqlConnection = databaseConnection.GetConnection();
                SqlCommand sqlCommand = databaseConnection.GetCommand("spOrderBook", sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@CartID", CartID);
                sqlCommand.Parameters.AddWithValue("@claimID", claimID);
                var OrderData = new ROrderModel();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {

                    status = sqlDataReader.GetInt32(0);
                    if (status == -1)
                    {
                        throw new Exception(OrderException.ExceptionType.BOOK_NOT_AVAILABLE.ToString());
                    }
                    
                    OrderData.CartID = Convert.ToInt32(sqlDataReader["CartID"]);
                    OrderData.BookID = Convert.ToInt32(sqlDataReader["BookID"]);
                    OrderData.UserID = Convert.ToInt32(sqlDataReader["UserID"]);
                    OrderData.AddressID = Convert.ToInt32(sqlDataReader["AddressID"]);
                    OrderData.Locality = sqlDataReader["Locality"].ToString();
                    OrderData.City = sqlDataReader["City"].ToString();
                    OrderData.State = sqlDataReader["State"].ToString();
                    OrderData.PhoneNumber = sqlDataReader["PhoneNumber"].ToString();
                    OrderData.Pincode = sqlDataReader["Pincode"].ToString();
                    OrderData.LandMark = sqlDataReader["LandMark"].ToString();
                    OrderData.CreatedDate = sqlDataReader["ModificationDate"].ToString();
                }
                sqlConnection.Close();
                if (OrderData.CartID > 0)
                {
                    return OrderData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public List<RAllOrderModel> OrderAllBook(int claimID)
        {
            try
            {
                int status;
                
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                SqlConnection sqlConnection = databaseConnection.GetConnection();
                SqlCommand sqlCommand = databaseConnection.GetCommand("spOrderAllBook", sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@claimID", claimID);
                var OrderAllData = new List<RAllOrderModel>();
                
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {

                    status = sqlDataReader.GetInt32(0);
                    if (status == -1)
                    {
                        throw new Exception(OrderException.ExceptionType.BOOK_NOT_AVAILABLE.ToString());
                    }
                    var OrderData = new RAllOrderModel();
                    OrderData.Status = Convert.ToBoolean(sqlDataReader["IsActive"]) == false ? "Book Order Successfully" : "Book Order UnSuccessfully";
                    OrderData.CartID = Convert.ToInt32(sqlDataReader["CartID"]);
                    OrderData.BookID = Convert.ToInt32(sqlDataReader["BookID"]);
                    OrderData.UserID = Convert.ToInt32(sqlDataReader["UserID"]);
                    OrderData.AddressID = Convert.ToInt32(sqlDataReader["AddressID"]);
                    OrderData.Locality = sqlDataReader["Locality"].ToString();
                    OrderData.City = sqlDataReader["City"].ToString();
                    OrderData.State = sqlDataReader["State"].ToString();
                    OrderData.PhoneNumber = sqlDataReader["PhoneNumber"].ToString();
                    OrderData.Pincode = sqlDataReader["Pincode"].ToString();
                    OrderData.LandMark = sqlDataReader["LandMark"].ToString();
                    OrderData.CreatedDate = sqlDataReader["ModificationDate"].ToString();
                    OrderAllData.Add(OrderData);
                }
                sqlConnection.Close();
                if (OrderAllData != null)
                {
                    return OrderAllData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public List<ROrderDetailModel> GetAllOrder(int userId)
        {
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                List<StoredProcedureParameter> paramList = new List<StoredProcedureParameter>();
                paramList.Add(new StoredProcedureParameter("@UserId", userId));
                DataTable table = databaseConnection.StoredProcedureExecuteReader("spGetUserOrder", paramList);
                //var cartData = new RAllCartDetail();
                List<ROrderDetailModel> cartLists = new List<ROrderDetailModel>();

                foreach (DataRow dataRow in table.Rows)
                {
                    ROrderDetailModel cartData = new ROrderDetailModel();
                    cartData.CartID = Convert.ToInt32(dataRow["CartID"]);
                    cartData.OrderID = Convert.ToInt32(dataRow["OrderID"]);
                    cartData.UserID = Convert.ToInt32(dataRow["UserID"]);
                    cartData.AddressID = Convert.ToInt32(dataRow["AddressID"]);
                    cartData.IsActive = Convert.ToBoolean(dataRow["IsActive"])==true?"Yes":"NO";
                    cartData.IsPlaced = Convert.ToBoolean(dataRow["IsPlaced"]) == true ? "NO" : "YES";
                    cartData.Quantity = Convert.ToInt32(dataRow["Quantity"]);
                    cartData.TotalPrice = Convert.ToInt32(dataRow["TotalPrice"]);
                    cartData.CreatedDate = dataRow["ModificationDate"].ToString();
                    cartLists.Add(cartData);
                }
                if (cartLists.Count != 0)
                {
                    return cartLists;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public RUpdateAddressModel UpdateAddress(int claimID, UpdateAddressModel updateAddressModel)
        {
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                List<StoredProcedureParameter> paramList = new List<StoredProcedureParameter>();

                paramList.Add(new StoredProcedureParameter("@claimID", claimID));
                paramList.Add(new StoredProcedureParameter("@State", updateAddressModel.State));
                paramList.Add(new StoredProcedureParameter("@Pincode", updateAddressModel.Pincode));
                paramList.Add(new StoredProcedureParameter("@PhoneNumber", updateAddressModel.PhoneNumber));
                paramList.Add(new StoredProcedureParameter("@Locality", updateAddressModel.Locality));
                paramList.Add(new StoredProcedureParameter("@LandMark", updateAddressModel.LandMark));
                paramList.Add(new StoredProcedureParameter("@City", updateAddressModel.City));

                DataTable table = databaseConnection.StoredProcedureExecuteReader("spUpdateAddress", paramList);
                var OrderData = new RUpdateAddressModel();

                foreach (DataRow dataRow in table.Rows)
                {
                    OrderData = new RUpdateAddressModel();
                    OrderData.AddressID = Convert.ToInt32(dataRow["AddressID"]);
                    OrderData.UserID = Convert.ToInt32(dataRow["UserID"]);
                    OrderData.Locality = dataRow["Locality"].ToString();
                    OrderData.City = dataRow["City"].ToString();
                    OrderData.State = dataRow["State"].ToString();
                    OrderData.PhoneNumber = dataRow["PhoneNumber"].ToString();
                    OrderData.Pincode = dataRow["Pincode"].ToString();
                    OrderData.LandMark = dataRow["LandMark"].ToString();
                    OrderData.CreatedDate = dataRow["ModificationDate"].ToString();
                }
                if (OrderData.AddressID > 0)
                {
                    return OrderData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public RUpdateAddressModel GetAddress(int claimID)
        {
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                List<StoredProcedureParameter> paramList = new List<StoredProcedureParameter>();

                paramList.Add(new StoredProcedureParameter("@claimID", claimID));

                DataTable table = databaseConnection.StoredProcedureExecuteReader("spGetAddress", paramList);
                var OrderData = new RUpdateAddressModel();

                foreach (DataRow dataRow in table.Rows)
                {
                    OrderData = new RUpdateAddressModel();
                    OrderData.AddressID = Convert.ToInt32(dataRow["AddressID"]);
                    OrderData.UserID = Convert.ToInt32(dataRow["UserID"]);
                    OrderData.Locality = dataRow["Locality"].ToString();
                    OrderData.City = dataRow["City"].ToString();
                    OrderData.State = dataRow["State"].ToString();
                    OrderData.PhoneNumber = dataRow["PhoneNumber"].ToString();
                    OrderData.Pincode = dataRow["Pincode"].ToString();
                    OrderData.LandMark = dataRow["LandMark"].ToString();
                    OrderData.CreatedDate = dataRow["ModificationDate"].ToString();
                }
                if (OrderData.AddressID > 0)
                {
                    return OrderData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

    }
}
