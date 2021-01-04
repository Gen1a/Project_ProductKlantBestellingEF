using System;

namespace BusinessLayer.Exceptions
{
    public class ProductFactoryException : Exception
    {
        public ProductFactoryException()
        {
        }

        public ProductFactoryException(string message) : base(message)
        {
        }

        public ProductFactoryException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}


