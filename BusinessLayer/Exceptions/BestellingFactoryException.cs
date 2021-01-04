using System;

namespace BusinessLayer.Exceptions
{
    public class BestellingFactoryException : Exception
    {
        public BestellingFactoryException()
        {
        }

        public BestellingFactoryException(string message) : base(message)
        {
        }

        public BestellingFactoryException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}



