using System;

namespace BusinessLayer.Exceptions
{
    public class DbKlantManagerException : Exception
    {
        public DbKlantManagerException()
        {
        }

        public DbKlantManagerException(string message) : base(message)
        {
        }

        public DbKlantManagerException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
