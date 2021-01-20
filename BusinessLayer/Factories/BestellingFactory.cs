using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Factories
{
    public static class BestellingFactory
    {
        // Relevant as a Bestelling without Producten is meaningless?
        public static Bestelling MaakNieuweBestelling(DateTime datum, Klant klant, long id)
        {
            if (datum == null) throw new BestellingFactoryException("BestellingFactory: Datum mag niet leeg zijn");
            if (klant == null) throw new BestellingFactoryException("BestellingFactory: Klant mag niet leeg zijn");
            if (id <= 0) throw new BestellingFactoryException("BestellingFactory: Id moet groter dan 0 zijn");

            return new Bestelling(datum, klant, id);
        }

        public static Bestelling MaakNieuweBestelling(DateTime datum, Klant klant, Dictionary<Product, int> producten)
        {
            if (datum == null) throw new BestellingFactoryException("BestellingFactory: Datum mag niet leeg zijn");
            if (klant == null) throw new BestellingFactoryException("BestellingFactory: Klant mag niet leeg zijn");
            if (producten == null) throw new BestellingFactoryException("BestellingFactory: Producten mag niet leeg zijn");

            Bestelling b = new Bestelling(datum, klant);
            foreach (KeyValuePair<Product, int> kvp in producten)
            {
                b.VoegProductToe(kvp.Key, kvp.Value);
            }

            return b;
        }

        public static Bestelling MaakNieuweBestelling(DateTime datum, Klant klant, Dictionary<Product, int> producten, long id)
        {
            if (id < 0) throw new BestellingFactoryException("BestellingFactory: Id van bestelling is invalide");
            Bestelling b = MaakNieuweBestelling(datum, klant, producten);
            b.BestellingId = id;

            return b;
        }
    }
}
