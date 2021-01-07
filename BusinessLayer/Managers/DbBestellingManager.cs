using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace BusinessLayer.Managers
{
    public class DbBestellingManager : IDbManager<Bestelling>
    {
        public IReadOnlyList<Bestelling> HaalOp()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Bestelling> HaalOp(Func<Bestelling, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Bestelling HaalOp(long id)
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
