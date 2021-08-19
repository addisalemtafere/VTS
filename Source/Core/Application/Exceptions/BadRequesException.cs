using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class BadRequesException : ApplicationException
    {
        public BadRequesException(string message) : base(message)
        {

        }
    }
}
