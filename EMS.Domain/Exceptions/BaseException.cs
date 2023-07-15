using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.Exceptions
{
    /// <summary>
    /// Base Exception
    /// </summary>
    public class BaseException : Exception
    {
        /// <summary>
        /// Exception Code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Base Exception
        /// </summary>
        public BaseException()
        {

        }
        /// <summary>
        /// Base Exception
        /// </summary>
        /// <param name="message"></param>
        public BaseException(string message):base(String.Format(message))
        {

        }
    }
}
