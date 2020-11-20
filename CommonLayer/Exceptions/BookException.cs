using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Exceptions
{
    public class BookException : Exception
    {

        /// <summary>
        /// Enum For Exception types.
        /// </summary>
        public enum ExceptionType
        {
            
            NULL_EXCEPTION,
            EMPTY_EXCEPTION,
            INVALID_PAGE_COUNT,
            INVALID_PRICE,
            INVALID_QUANTITY,
            INVALID_BOOKID,
            BOOK_NOT_AVAILABLE,
            INVALID_ATTRIBUTE,
            INVALID_PRICE_DATA,
            INVALID_QUANTITY_DATA
        }

        /// <summary>
        /// Exception type Reference.
        /// </summary>
        ExceptionType type;

        /// <summary>
        /// Constrcutor For Setting Exception Type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public BookException(BookException.ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }

    }
}
