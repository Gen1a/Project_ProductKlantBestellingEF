using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Factories
{
    public static class KlantFactory
    {
        public static Klant MaakKlant(string naam, string adres, IDFactory idFactory)
        {
            try
            {
                return new Klant(naam.Trim(), adres.Trim(), idFactory.MaakKlantID());
            }
            catch (KlantException ex)
            {
                throw new KlantFactoryException("MaakKlant", ex);
            }
        }
        public static Klant MaakKlant(string naam, string adres, List<Bestelling> bestellingen, IDFactory idFactory)
        {
            try
            {
                return new Klant(naam.Trim(), adres.Trim(), idFactory.MaakKlantID(), bestellingen);
            }
            catch (KlantException ex)
            {
                throw new KlantFactoryException("MaakKlant", ex);
            }
        }
    }
}
