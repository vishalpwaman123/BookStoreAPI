using CommonLayer.Exceptions;
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
    public class CartRL : ICartRL
    {

        private static readonly string ConnectionDeclaration = "Server=.; Database=BookStoreDatabase; Trusted_Connection=true;MultipleActiveResultSets=True";

        SqlConnection sqlConnectionVariable = new SqlConnection(ConnectionDeclaration);

        /// <summary>
        /// create field for configuration
        /// </summary>
        IConfiguration configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public CartRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public RAllCartDetail AddToCart(int userId, int BookId)
        {
            try
            {
                int status = 0;
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                SqlConnection sqlConnection = databaseConnection.GetConnection();
                SqlCommand sqlCommand = databaseConnection.GetCommand("spAddCart", sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                sqlCommand.Parameters.AddWithValue("@BookId", BookId);
                RAllCartDetail usermodel = new RAllCartDetail();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    status = sqlDataReader.GetInt32(0);
                    if (status == 0)
                    {
                        return null; ;
                    }
                    else if (status == -1)
                    {
                        throw new Exception(BookException.ExceptionType.BOOK_NOT_AVAILABLE.ToString());
                    }
                    usermodel.UserID = Convert.ToInt32(sqlDataReader["UserID"]);
                    usermodel.CreatedDate = sqlDataReader["ModificationDate"].ToString();
                    usermodel.CartID = Convert.ToInt32(sqlDataReader["CartID"].ToString());
                    usermodel.IsActive = Convert.ToBoolean(sqlDataReader["IsActive"].ToString()) == false ? "No" : "Yes";
                    usermodel.BookID = Convert.ToInt32(sqlDataReader["BookID"].ToString());
                    return usermodel;
                    
                }
                sqlConnection.Close();
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<RAllCartDetail> GetAllCart(int userId)
        {
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                List<StoredProcedureParameter> paramList = new List<StoredProcedureParameter>();
                paramList.Add(new StoredProcedureParameter("@UserId", userId));
                DataTable table = databaseConnection.StoredProcedureExecuteReader("spGetUserCart", paramList);
                //var cartData = new RAllCartDetail();
                List<RAllCartDetail> cartLists = new List<RAllCartDetail>();

                foreach (DataRow dataRow in table.Rows)
                {
                    RAllCartDetail cartData = new RAllCartDetail();
                    cartData.CartID = (int)dataRow["CartID"];
                    cartData.UserID = Convert.ToInt32(dataRow["UserId"].ToString());
                    cartData.BookID = Convert.ToInt32(dataRow["BookId"].ToString());
                    cartData.IsActive = Convert.ToBoolean(dataRow["IsActive"].ToString())==true?"Yes":"No";
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public RAllCartDetail DeleteCart(int userId, int cartId)
        {
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                SqlConnection sqlConnection = databaseConnection.GetConnection();
                SqlCommand sqlCommand = databaseConnection.GetCommand("spDeleteCart", sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                sqlCommand.Parameters.AddWithValue("@CartId", cartId);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                RAllCartDetail cartData = new RAllCartDetail();
                while (sqlDataReader.Read())
                {
                    cartData.CartID = (int)sqlDataReader["CartID"];
                    cartData.UserID = Convert.ToInt32(sqlDataReader["UserId"].ToString());
                    cartData.BookID = Convert.ToInt32(sqlDataReader["BookId"].ToString());
                    cartData.IsActive = Convert.ToBoolean(sqlDataReader["IsActive"].ToString()) == true ? "Yes" : "No";
                    cartData.CreatedDate = sqlDataReader["ModificationDate"].ToString();
                    if (cartData.CartID > 0)
                    {
                        return cartData;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
