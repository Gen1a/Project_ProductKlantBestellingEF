using System;

namespace BusinessLayer.Exceptions
{
    public class DbBestellingManagerException : Exception
    {
        public DbBestellingManagerException()
        {
        }

        public DbBestellingManagerException(string message) : base(message)
        {
        }

        public DbBestellingManagerException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

