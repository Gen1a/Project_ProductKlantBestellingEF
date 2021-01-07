using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;

namespace BusinessLayer.Managers
{
    public class DbKlantManager : IDbManager<Klant>
    {
        public IReadOnlyList<Klant> HaalOp()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Klant> HaalOp(Func<Klant, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Klant HaalOp(long id)
        {
            throw new NotImplementedException();
        }

        public void Verwijder(Klant item)
        {
            throw new NotImplementedException();
        }

        public void VoegToe(Klant item)
        {
            throw new NotImplementedException();
        }
    }
}
