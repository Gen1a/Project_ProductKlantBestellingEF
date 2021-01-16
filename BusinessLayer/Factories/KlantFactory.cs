using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Factories
{
    public static class KlantFactory
    {
        public static Klant MaakNieuweKlant(string naam, string adres)
        {
            if (string.IsNullOrEmpty(naam)) throw new KlantFactoryException("KlantFactory: Naam van een klant mag niet leeg zijn");
            if (string.IsNullOrEmpty(adres)) throw new KlantFactoryException("KlantFactory: Adres van een klant mag niet leeg zijn");

            return new Klant(naam, adres);
        }

        public static Klant MaakNieuweKlant(string naam, string adres, long id)
        {
            if (id < 0) throw new KlantFactoryException("KlantFactory: Id van klant is invalide");
            Klant k = MaakNieuweKlant(naam, adres);
            k.KlantId = id;

            return k;
        }
    }
}
