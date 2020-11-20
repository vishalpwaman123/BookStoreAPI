using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IBookBL
    {

        /// <summary>
        /// Declare Abstration For add Book
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        RBookAddModel AddBook(BookModel bookModel , int claimId);

        /*/// <summary>
        /// 
        /// </summary>
        /// <param name="BookID"></param>
        /// <param name="Image"></param>
        /// <returns></returns>
        string AddImage( IFormFile Image);
*/
        /*/// <summary>
        /// 
        /// </summary>
        /// <param name="BookID"></param>
        /// <param name="Image"></param>
        /// <returns></returns>
        RImageAddModel UpdateImage(int AdminID, int BookID, IFormFile Image);
*/
        /// <summary>
        /// Declare Abstration For Search Book
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        RBookAddModel SearchBook(string bookName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        List<RBookAddModel> SortBook( int Flag);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Flag"></param>
        /// <returns></returns>
        List<RBookAddModel> BookFilter(int FirstPrice, int FinalPrice);

        /// <summary>
        /// Declare Abstration For Search Book
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        List<RBookAddModel> GetAllBooks();

        /// <summary>
        /// Declare Abstration For Update Book Price
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        RBookAddModel UpdateBook(UpdateBookModel updateBookModel, int claimId);

        /// <summary>
        /// Declare Abstration For Update Book Price
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        RBookAddModel DeleteBook(int BookId, int claimId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimID"></param>
        /// <returns></returns>
        List<ROutOfStockModel> SearchOutOfStockBook();

    }
}
