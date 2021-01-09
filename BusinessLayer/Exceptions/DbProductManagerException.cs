using System;

namespace BusinessLayer.Exceptions
{
    public class DbProductManagerException : Exception
    {
        public DbProductManagerException()
        {
        }

        public DbProductManagerException(string message) : base(message)
        {
        }

        public DbProductManagerException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

