using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Exceptions
{
   public class OrderException : Exception
    {

        /// <summary>
        /// Enum For Exception types.
        /// </summary>
        public enum ExceptionType
        {
            BOOK_NOT_AVAILABLE,
            NULL_EXCEPTION,
            EMPTY_EXCEPTION,
            INVALID_CARTID
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
        public OrderException(OrderException.ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }


    }
}
