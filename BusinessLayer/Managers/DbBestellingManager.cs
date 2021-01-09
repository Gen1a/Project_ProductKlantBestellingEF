using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace BusinessLayer.Managers
{
    public class DbBestellingManager : IDbManager<Bestelling>
    {
        // readonly possible if instance constructor of the class contains the instance field declaration
        private readonly string connectionString;

        public DbBestellingManager(string connection)
        {
            if (string.IsNullOrEmpty(connection))
            {
                throw new DbBestellingManagerException("Fout bij het aanmaken van DbBestellingManager: connectionstring moet ingevuld zijn");
            }
            this.connectionString = connection;
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public IReadOnlyList<Bestelling> HaalOp()
        {
            throw new NotImplementedException();
        }
        public Bestelling HaalOp(long id)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Bestelling> HaalOp(Func<Bestelling, bool> predicate)
        {
            throw new NotImplementedException();
        }

        

        public void Verwijder(Bestelling item)
        {
            throw new NotImplementedException();
        }

        public void VoegToe(Bestelling item)
        {
            throw new NotImplementedException();
        }
    }
}
