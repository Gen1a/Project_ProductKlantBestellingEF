using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Managers
{
    class DbProductManager : IDbManager<Product>
    {
        public IReadOnlyList<Product> HaalOp()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Product> HaalOp(Func<Product, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Product HaalOp(long id)
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
