using System;

namespace BusinessLayer.Exceptions
{
    public class DbProduct_BestellingManagerException : Exception
    {
        public DbProduct_BestellingManagerException()
        {
        }

        public DbProduct_BestellingManagerException(string message) : base(message)
        {
        }

        public DbProduct_BestellingManagerException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
