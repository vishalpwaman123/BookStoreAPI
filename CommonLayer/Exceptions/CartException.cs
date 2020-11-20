using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Exceptions
{
    public class CartException : Exception
    {

        /// <summary>
        /// Enum For Exception types.
        /// </summary>
        public enum ExceptionType
        {
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
        public CartException(CartException.ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }


    }
}
