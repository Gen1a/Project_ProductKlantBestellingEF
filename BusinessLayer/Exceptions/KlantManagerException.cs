using System;

namespace BusinessLayer.Exceptions
{
    public class KlantManagerException : Exception
    {
        public KlantManagerException()
        {
        }

        public KlantManagerException(string message) : base(message)
        {
        }

        public KlantManagerException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
