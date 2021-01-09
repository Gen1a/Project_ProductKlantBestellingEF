using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BusinessLayer.Managers
{
    public class DbProductManager : IDbManager<Product>
    {
        // readonly possible if instance constructor of the class contains the instance field declaration
        private readonly string connectionString;

        public DbProductManager(string connection)
        {
            if (string.IsNullOrEmpty(connection))
            {
                throw new DbProductManagerException("Fout bij het aanmaken van DbProductManager: connectionstring moet ingevuld zijn");
            }
            this.connectionString = connection;
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public IReadOnlyList<Product> HaalOp()
        {
            throw new NotImplementedException();
        }
        public Product HaalOp(long id)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Product> HaalOp(Func<Product, bool> predicate)
        {
            throw new NotImplementedException();
        }


        public void Verwijder(Product item)
        {
            throw new NotImplementedException();
        }

        public void VoegToe(Product item)
        {
            throw new NotImplementedException();
        }
    }
}
