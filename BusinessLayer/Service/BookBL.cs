using BusinessLayer.Interface;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class BookBL : IBookBL
    {

        /// <summary>
        /// RL Reference.
        /// </summary>
        private IBookRL bookRL;

        /// <summary>
        /// Constructor For Setting UserRL Instance.
        /// </summary>
        /// <param name="userRL"></param>
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        /// <summary>
        /// Function For Register User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public RBookAddModel AddBook(BookModel bookModel, int ClaimId)
        {
            try
            {
                return this.bookRL.AddBook(bookModel, ClaimId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AdminID"></param>
        /// <param name="BookID"></param>
        /// <param name="Image"></param>
        /// <returns></returns>
       /* public RImageAddModel AddImage(int AdminID, int BookID, IFormFile Image)
        {
            try
            {
                return this.bookRL.AddImage(AdminID,BookID, Image);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }*/

       /* /// <summary>
        /// 
        /// </summary>
        /// <param name="AdminID"></param>
        /// <param name="BookID"></param>
        /// <param name="Image"></param>
        /// <returns></returns>
        public RImageAddModel UpdateImage(int AdminID, int BookID, IFormFile Image)
        {
            try
            {
                return this.bookRL.AddImage(AdminID, BookID, Image);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
*/
        /// <summary>
        /// Function For Register User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public RBookAddModel SearchBook(string Bookname)
        {
            try
            {
                return this.bookRL.SearchBook(Bookname);
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
        public List<RBookAddModel> GetAllBooks()
        {
            try
            {
                return this.bookRL.GetAllBooks();
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
        public List<RBookAddModel> SortBook( int Flag, int subflag)
        {
            try
            {
                return this.bookRL.SortBook( Flag, subflag);
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
        public List<RBookAddModel> BookFilter(int FirstPrice, int FinalPrice)
        {
            try
            {
                return this.bookRL.BookFilter(FirstPrice, FinalPrice);
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
        public RBookAddModel UpdateBook(UpdateBookModel updateBookModel, int claimId)
        {
            try
            {
                return this.bookRL.UpdateBook(updateBookModel, claimId);
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
        public RBookAddModel DeleteBook(int BookId, int claimId)
        {
            try
            {
                return this.bookRL.DeleteBook(BookId, claimId);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<ROutOfStockModel> SearchOutOfStockBook()
        {
            try
            {
                return this.bookRL.SearchOutOfStockBook();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
