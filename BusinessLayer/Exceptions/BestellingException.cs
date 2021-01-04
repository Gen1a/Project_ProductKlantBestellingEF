using System;

namespace BusinessLayer.Exceptions
{
    public class BestellingException : Exception
    {
        public BestellingException()
        {
        }

        public BestellingException(string message) : base(message)
        {
        }

        public BestellingException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
