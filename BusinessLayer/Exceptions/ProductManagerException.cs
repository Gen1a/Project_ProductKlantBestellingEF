using System;

namespace BusinessLayer.Exceptions
{
    public class ProductManagerException : Exception
    {
        public ProductManagerException()
        {
        }

        public ProductManagerException(string message) : base(message)
        {
        }

        public ProductManagerException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
