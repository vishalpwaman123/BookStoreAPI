using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Exceptions;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class BookRL : IBookRL
    {

        /// <summary>
        /// create field for configuration 
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initialzes the memory and inject the configuration interface
        /// </summary>
        /// <param name="configuration"></param>
        public BookRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// add book
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        public RBookAddModel AddBook(BookModel bookModel, int claimId)
        {
            try
            { 
                string ImageLink = AddImage(bookModel.Image);
                int status = 0;
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                List<StoredProcedureParameter> paramList = new List<StoredProcedureParameter>();
                paramList.Add(new StoredProcedureParameter("@BookName", bookModel.BookName));
                paramList.Add(new StoredProcedureParameter("@AdminID", claimId));
                paramList.Add(new StoredProcedureParameter("@AuthorName", bookModel.AuthorName));
                paramList.Add(new StoredProcedureParameter("@Description", bookModel.Description));
                paramList.Add(new StoredProcedureParameter("@Price", bookModel.Price));
                paramList.Add(new StoredProcedureParameter("@Pages", bookModel.Pages));
                paramList.Add(new StoredProcedureParameter("@Quantity", bookModel.Quantity));
                paramList.Add(new StoredProcedureParameter("@Image", ImageLink));
                DataTable table = databaseConnection.StoredProcedureExecuteReader("spAddBook", paramList);
                var bookData = new RBookAddModel();

                foreach (DataRow dataRow in table.Rows)
                {
                    bookData = new RBookAddModel();
                    status = (int)dataRow["BookID"];
                    if (status == 0)
                    {
                        return null;
                    }
                    bookData.BookID = (int)dataRow["BookID"];
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
                return bookData;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// add book
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        public string AddImage(IFormFile Image)
        {
            try
            {
                int status = 0;
                Account account = new Account(
                                configuration["CloudinarySettings:CloudName"],
                                configuration["CloudinarySettings:ApiKey"],
                                configuration["CloudinarySettings:ApiSecret"]);
                var path = Image.OpenReadStream();
                Cloudinary cloudinary = new Cloudinary(account);

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(Image.FileName, path)
                };

                var uploadResult =  cloudinary.Upload(uploadParams);

                /*   int Flag = 0;
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                SqlConnection sqlConnection = databaseConnection.GetConnection();
                SqlCommand sqlCommand = databaseConnection.GetCommand("spAddImage", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@BookID", BookID);
                //sqlCommand.Parameters.AddWithValue("@Flag", Flag);
                sqlCommand.Parameters.AddWithValue("@AdminID", AdminID);*//*
                sqlCommand.Parameters.AddWithValue("@Image", uploadResult.Url.ToString());
                *//*sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                RImageAddModel bookAddModel = new RImageAddModel();
                while (sqlDataReader.Read())
                {
                    status = sqlDataReader.GetInt32(0);
                    if (status == 0)
                    {
                        return null;
                    }  
                    bookAddModel.BookID = (int)sqlDataReader["BookID"];
                    bookAddModel.AdminID = (int)sqlDataReader["AdminID"];
                    bookAddModel.BookName = sqlDataReader["BookName"].ToString();
                    bookAddModel.AuthorName = sqlDataReader["AuthorName"].ToString();
                    bookAddModel.Description = sqlDataReader["Description"].ToString();
                    bookAddModel.Price = Convert.ToInt32(sqlDataReader["Price"]);
                    bookAddModel.Pages = Convert.ToInt32(sqlDataReader["Pages"]);
                    bookAddModel.CreatedDate = sqlDataReader["ModificationDate"].ToString();
                    bookAddModel.Quantity = Convert.ToInt32(sqlDataReader["Quantity"]);
                    bookAddModel.ImageLink = sqlDataReader["Image"].ToString() == "" ? "Not Available" : sqlDataReader["Image"].ToString();
                    bookAddModel.Updater_AdminId = Convert.ToInt32(sqlDataReader["Updater_AdminId"]);
                    if (bookAddModel.BookID == BookID)
                    {
                        return bookAddModel;
                    }
                }*/
                return uploadResult.Url.ToString();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// search books by book title
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        public RBookAddModel SearchBook(string bookName)
        {
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                List<StoredProcedureParameter> paramList = new List<StoredProcedureParameter>();
                paramList.Add(new StoredProcedureParameter("@BookName", bookName));
              
                DataTable table = databaseConnection.StoredProcedureExecuteReader("spSearchBookByName", paramList);
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
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// search books by book title
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        public List<RBookAddModel> BookFilter(int FirstPrice, int FinalPrice)
        {
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                List<StoredProcedureParameter> paramList = new List<StoredProcedureParameter>();
                paramList.Add(new StoredProcedureParameter("@FirstPrice", FirstPrice));
                paramList.Add(new StoredProcedureParameter("@FinalPrice", FinalPrice));
                DataTable table = databaseConnection.StoredProcedureExecuteReader("spBookFilter", paramList);
                var bookData = new RBookAddModel();
                List<RBookAddModel> bookList = new List<RBookAddModel>();
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
                    bookList.Add(bookData);
                }
                if (bookList != null)
                {
                    return bookList;
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
        /// get all books 
        /// </summary>
        /// <returns></returns>
        public List<RBookAddModel> GetAllBooks()
        {
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                IList<StoredProcedureParameter> paramList = new List<StoredProcedureParameter>();

                DataTable table = databaseConnection.StoredProcedureExecuteReader("spGetAllBooks", paramList);
                var bookData = new RBookAddModel();
                IList<RBookAddModel> bookList = new List<RBookAddModel>();
                
                foreach (DataRow dataRow in table.Rows)
                {
                    bookData = new RBookAddModel();
                    bookData.BookID = (int)dataRow["BookID"];
                    bookData.AdminID = (int)dataRow["AdminID"];
                    bookData.BookName = dataRow["BookName"].ToString();
                    bookData.AuthorName = dataRow["AuthorName"].ToString();
                    bookData.Description = dataRow["Description"].ToString();
                    bookData.Quantity = Convert.ToInt32(dataRow["Quantity"]);
                    bookData.Price = Convert.ToInt32(dataRow["Price"]);
                    bookData.Pages = Convert.ToInt32(dataRow["Pages"]);
                    bookData.CreatedDate = dataRow["ModificationDate"].ToString();
                    bookData.ImageLink = dataRow["Image"].ToString()==""?"Not Available": dataRow["Image"].ToString();
                    bookData.Updater_AdminId = Convert.ToInt32(dataRow["Updater_AdminId"]);
                    bookList.Add(bookData);
                }
                if (bookList != null)
                {
                    return bookList.ToList();
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
        /// get all books 
        /// </summary>
        /// <returns></returns>
        public List<RBookAddModel> SortBook(int Flag)
        {
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                IList<StoredProcedureParameter> paramList = new List<StoredProcedureParameter>();

                //paramList.Add(new StoredProcedureParameter("@Attribute", attribute));
                paramList.Add(new StoredProcedureParameter("@Flag", Flag));
                DataTable table = databaseConnection.StoredProcedureExecuteReader("spGetAllSortBooks", paramList);

                var bookData = new RBookAddModel();
                IList<RBookAddModel> bookList = new List<RBookAddModel>();
                foreach (DataRow dataRow in table.Rows)
                {
                    bookData = new RBookAddModel();
                    bookData.BookID = (int)dataRow["BookID"];
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
                    bookList.Add(bookData);
                }
                if (bookList != null)
                {
                    return bookList.ToList();
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
        /// update book by book price
        /// </summary>
        /// <param name="updateBookModel"></param>
        /// <returns></returns>
        public RBookAddModel UpdateBook(UpdateBookModel updateBookModel, int claimId)
        {
            try
            {
                string ImageLink = AddImage(updateBookModel.Image);
                int status = 0;
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                List<StoredProcedureParameter> paramList = new List<StoredProcedureParameter>();
                paramList.Add(new StoredProcedureParameter("@BookName", updateBookModel.BookName));
                paramList.Add(new StoredProcedureParameter("@BookId", updateBookModel.BookId));
                paramList.Add(new StoredProcedureParameter("@AdminID", claimId));
                paramList.Add(new StoredProcedureParameter("@AuthorName", updateBookModel.AuthorName));
                paramList.Add(new StoredProcedureParameter("@Description", updateBookModel.Description));
                paramList.Add(new StoredProcedureParameter("@Price", updateBookModel.Price));
                paramList.Add(new StoredProcedureParameter("@Pages", updateBookModel.Pages));
                paramList.Add(new StoredProcedureParameter("@Quantity", updateBookModel.Quantity));
                paramList.Add(new StoredProcedureParameter("@Image", ImageLink));
                DataTable table = databaseConnection.StoredProcedureExecuteReader("spUpdateBook", paramList);
                var bookData = new RBookAddModel();

                foreach (DataRow dataRow in table.Rows)
                {
                    bookData = new RBookAddModel();
                    status = (int)dataRow["BookID"];
                    if (status == 0)
                    {
                        return null;
                    }
                    bookData.BookID = (int)dataRow["BookID"];
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
                return bookData;
                /* int status;
                 DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                 SqlConnection sqlConnection = databaseConnection.GetConnection();
                 SqlCommand sqlCommand = databaseConnection.GetCommand("spUpdateBook", sqlConnection);
                 sqlCommand.Parameters.AddWithValue("@BookId", updateBookModel.BookId);
                 //sqlCommand.Parameters.AddWithValue("@AttributeName", updateBookModel.AttributeName);

                 sqlCommand.Parameters.AddWithValue("@AdminID", claimId);
  *//*               sqlCommand.Parameters.AddWithValue("@Flag", flag);*//*
                 sqlConnection.Open();
                 SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                 RBookAddModel bookAddModel = new RBookAddModel();
                 while (sqlDataReader.Read())
                 {
                     bookAddModel = new RBookAddModel();
                     status = sqlDataReader.GetInt32(0);
                     if (status == 0)
                     {
                         throw new Exception(BookException.ExceptionType.INVALID_BOOKID.ToString());
                     }
                     bookAddModel.BookID = (int)sqlDataReader["BookID"];
                     bookAddModel.AdminID = (int)sqlDataReader["AdminID"];
                     bookAddModel.BookName = sqlDataReader["BookName"].ToString();
                     bookAddModel.AuthorName = sqlDataReader["AuthorName"].ToString();
                     bookAddModel.Description = sqlDataReader["Description"].ToString();
                     bookAddModel.Price = Convert.ToInt32(sqlDataReader["Price"]);
                     bookAddModel.Pages = Convert.ToInt32(sqlDataReader["Pages"]);
                     bookAddModel.CreatedDate = sqlDataReader["ModificationDate"].ToString();
                     bookAddModel.Quantity = Convert.ToInt32(sqlDataReader["Quantity"]);
                     bookAddModel.ImageLink = sqlDataReader["Image"].ToString() == "" ? "Not Available" : sqlDataReader["Image"].ToString();
                     bookAddModel.Updater_AdminId = Convert.ToInt32(sqlDataReader["Updater_AdminId"]);
                     if (bookAddModel.BookID == updateBookModel.BookId)
                     {
                         return bookAddModel;
                     }
                 }
                 sqlConnection.Close();
                 return null;*/
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// delete book
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public RBookAddModel DeleteBook(int bookId , int claimId)
        {
            try
            {
                int status = 0;
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                SqlConnection sqlConnection = databaseConnection.GetConnection();
                SqlCommand sqlCommand = databaseConnection.GetCommand("spDeleteBook", sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                sqlCommand.Parameters.AddWithValue("@AdminID", claimId);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                RBookAddModel bookAddModel = new RBookAddModel();
                while (sqlDataReader.Read())
                {
                    bookAddModel = new RBookAddModel();
                    status = sqlDataReader.GetInt32(0);
                    if (status == 0)
                    {
                        return null;
                    }
                    bookAddModel.BookID = (int)sqlDataReader["BookID"];
                    //bookAddModel.AdminID = (int)sqlDataReader["AdminID"];
                    bookAddModel.BookName = sqlDataReader["BookName"].ToString();
                    bookAddModel.AuthorName = sqlDataReader["AuthorName"].ToString();
                    bookAddModel.Description = sqlDataReader["Description"].ToString();
                    bookAddModel.Price = Convert.ToInt32(sqlDataReader["Price"]);
                    bookAddModel.Pages = Convert.ToInt32(sqlDataReader["Pages"]);
                    bookAddModel.CreatedDate = sqlDataReader["ModificationDate"].ToString();
                    bookAddModel.Quantity = Convert.ToInt32(sqlDataReader["Quantity"]);
                    bookAddModel.ImageLink = sqlDataReader["Image"].ToString() == "" ? "Not Available" : sqlDataReader["Image"].ToString();
                    bookAddModel.Updater_AdminId = Convert.ToInt32(sqlDataReader["Updater_AdminId"]);
                    if (bookAddModel.BookID == bookId)
                    {
                        return bookAddModel;
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

        public List<ROutOfStockModel> SearchOutOfStockBook()
        {
            try
            {
                DatabaseConnection databaseConnection = new DatabaseConnection(this.configuration);
                IList<StoredProcedureParameter> paramList = new List<StoredProcedureParameter>();

                DataTable table = databaseConnection.StoredProcedureExecuteReader("spGetOutOfStockBook", paramList);
                var bookData = new ROutOfStockModel();
                IList<ROutOfStockModel> bookList = new List<ROutOfStockModel>();
                foreach (DataRow dataRow in table.Rows)
                {
                    bookData = new ROutOfStockModel();
                    bookData.BookId = (int)dataRow["BookID"];
                    if(bookData.BookId ==0)
                    {
                        return null;
                    }
                    bookData.BookName = dataRow["BookName"].ToString();
                    bookData.AuthorName = dataRow["AuthorName"].ToString();
                    bookData.ToOutOfStockTime = dataRow["ModificationDate"].ToString();
                    bookList.Add(bookData);
                }
                if (bookList != null)
                {
                    return bookList.ToList();
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
