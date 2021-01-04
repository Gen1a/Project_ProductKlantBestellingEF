using System;

namespace BusinessLayer.Exceptions
{
    public class KlantFactoryException : Exception
    {
        public KlantFactoryException()
        {
        }

        public KlantFactoryException(string message) : base(message)
        {
        }

        public KlantFactoryException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}



