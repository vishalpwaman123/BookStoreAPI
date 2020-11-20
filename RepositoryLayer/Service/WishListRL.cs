using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CommonLayer.Exceptions;

namespace RepositoryLayer.Service
{
    public class WishListRL : IWishListRL
    {

        IConfiguration configuration;
        public WishListRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public RAddWishListModel AddBookToWishList(int userId, int BookId)
        {
            try
            {
                
                int status = 0;
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                SqlConnection sqlConnection = databaseConnection.GetConnection();
                SqlCommand sqlCommand = databaseConnection.GetCommand("spBookAddToWishList", sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                sqlCommand.Parameters.AddWithValue("@BookId", BookId);
                var wishListData = new RAddWishListModel();
                
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    status = sqlDataReader.GetInt32(0);
                    if (status == 0)
                    {
                        return null;
                    }else if (status == -1 )
                    {
                        throw new Exception(WishListException.ExceptionType.BOOK_NOT_AVAILABLE.ToString());
                    }
                    wishListData.UserId = Convert.ToInt32(sqlDataReader["UserID"]);
                    wishListData.CreatedDate = Convert.ToDateTime(sqlDataReader["ModificationDate"].ToString());
                    wishListData.WishListId = Convert.ToInt32(sqlDataReader["WishListId"].ToString());
                    wishListData.IsMoved = Convert.ToBoolean(sqlDataReader["IsMoved"].ToString()) == true ? "yes" : "No";
                    wishListData.BookId = Convert.ToInt32(sqlDataReader["BookId"].ToString());
                    return wishListData;
                    
                }
                sqlConnection.Close();

                if ( !wishListData.Equals(null))
                {
                    return wishListData;
                }else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }


        public List<RAddWishListModel> GetAllWishList(int userId)
        {
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                List<StoredProcedureParameter> paramList = new List<StoredProcedureParameter>();
                paramList.Add(new StoredProcedureParameter("@UserId", userId));
                DataTable table = databaseConnection.StoredProcedureExecuteReader("GetUserWishList", paramList);
                //var wishListData = new RAddWishListModel();
                List<RAddWishListModel> wishLists = new List<RAddWishListModel>();

                foreach (DataRow dataRow in table.Rows)
                {
                    RAddWishListModel wishListData = new RAddWishListModel();
                    wishListData.WishListId = (int)dataRow["WishListId"];
                    wishListData.UserId = Convert.ToInt32(dataRow["UserId"].ToString());
                    wishListData.BookId = Convert.ToInt32(dataRow["BookId"].ToString());
                    wishListData.IsMoved = Convert.ToBoolean(dataRow["IsMoved"].ToString()) == true ? "Yes" : "No";
                    wishListData.CreatedDate = Convert.ToDateTime(dataRow["ModificationDate"]);
                    wishListData.BookDetail = SearchBookById(wishListData.BookId);
                    wishLists.Add(wishListData);
                }
                if (wishLists.Count != 0)
                {
                    return wishLists;
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


        public RAddWishListModel DeleteWishList(int userId, int wishListId)
        {
            try
            {
                int status = 0;
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                SqlConnection sqlConnection = databaseConnection.GetConnection();
                SqlCommand sqlCommand = databaseConnection.GetCommand("spDeleteWishList", sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                sqlCommand.Parameters.AddWithValue("@WishListId", wishListId);
                RAddWishListModel usermodel = new RAddWishListModel();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    status = sqlDataReader.GetInt32(0);
                    usermodel.UserId = Convert.ToInt32(sqlDataReader["UserID"]);
                    usermodel.CreatedDate = Convert.ToDateTime(sqlDataReader["ModificationDate"].ToString());
                    usermodel.WishListId = Convert.ToInt32(sqlDataReader["WishListId"].ToString());
                    usermodel.IsMoved = Convert.ToBoolean(sqlDataReader["IsMoved"].ToString())==true?"No":"Yes";
                    usermodel.BookId = Convert.ToInt32(sqlDataReader["BookId"].ToString());
                    if (status > 0)
                    {
                        return usermodel;
                    }    
                }
                sqlConnection.Close();
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public RAddWishListModel WishListMoveToCart(int userId, int wishListId)
        {
            try
            {
                int status = 0;
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                SqlConnection sqlConnection = databaseConnection.GetConnection();
                SqlCommand sqlCommand = databaseConnection.GetCommand("spMoveWishListToCart", sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                sqlCommand.Parameters.AddWithValue("@WishListId", wishListId);
                RAddWishListModel usermodel = new RAddWishListModel();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    status = sqlDataReader.GetInt32(0);
                    usermodel.UserId = Convert.ToInt32(sqlDataReader["UserID"]);
                    usermodel.CreatedDate = Convert.ToDateTime(sqlDataReader["ModificationDate"].ToString());
                    usermodel.WishListId = Convert.ToInt32(sqlDataReader["WishListId"].ToString());
                    usermodel.IsMoved = Convert.ToBoolean(sqlDataReader["IsMoved"].ToString()) == false ? "No" : "Yes";
                    usermodel.BookId = Convert.ToInt32(sqlDataReader["BookId"].ToString());
                    if (status > 0)
                    {
                        return usermodel;
                    }
                }
                sqlConnection.Close();
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// search books by book title
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        public RBookAddModel SearchBookById(int bookId)
        {
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                List<StoredProcedureParameter> paramList = new List<StoredProcedureParameter>();
                paramList.Add(new StoredProcedureParameter("@BookId", bookId));

                DataTable table = databaseConnection.StoredProcedureExecuteReader("spSearchBookById", paramList);
                var bookData = new RBookAddModel();
                IList<RBookAddModel> bookList = new List<RBookAddModel>();
                bookData = null;
                foreach (DataRow dataRow in table.Rows)
                {
                    bookData = new RBookAddModel();
                    bookData.BookID = (int)dataRow["BookID"];
                    if (bookData.BookID == 0)
                    {
                        return null;
                    }
                    bookData.AdminID = (int)dataRow["AdminID"];
                    bookData.BookName = dataRow["BookName"].ToString();
                    bookData.AuthorName = dataRow["AuthorName"].ToString();
                    bookData.Description = dataRow["Description"].ToString();
                    bookData.Quantity = Convert.ToInt32(dataRow["Quantity"]);
                    bookData.Price = Convert.ToInt32(dataRow["Price"]);
                    bookData.Pages = Convert.ToInt32(dataRow["Pages"]);
                    bookData.CreatedDate = dataRow["ModificationDate"].ToString();
                    bookData.ImageLink = dataRow["Image"].ToString() == "" ? "Not Available" : dataRow["Image"].ToString();
                    bookData.Updater_AdminId = Convert.ToInt32(dataRow["Updater_AdminId"]);
                }
                if (bookData != null)
                {
                    return bookData;
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

    }
}
