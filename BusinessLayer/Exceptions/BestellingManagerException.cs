using System;

namespace BusinessLayer.Exceptions
{
    public class BestellingManagerException : Exception
    {
        public BestellingManagerException()
        {
        }

        public BestellingManagerException(string message) : base(message)
        {
        }

        public BestellingManagerException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

