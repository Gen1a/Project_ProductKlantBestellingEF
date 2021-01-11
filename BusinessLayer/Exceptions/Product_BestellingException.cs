using System;

namespace BusinessLayer.Exceptions
{
    public class Product_BestellingException : Exception
    {
        public Product_BestellingException()
        {
        }

        public Product_BestellingException(string message) : base(message)
        {
        }

        public Product_BestellingException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

