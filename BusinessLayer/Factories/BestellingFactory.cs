using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Factories
{
    public static class BestellingFactory
    {
        public static Bestelling MaakBestelling(IDFactory idFactory)
        {
            try
            {
                return new Bestelling(idFactory.MaakBestellingID(), DateTime.Now);
            }
            catch (BestellingException ex)
            {
                throw new BestellingFactoryException("MaakBestelling", ex);
            }
        }
        public static Bestelling MaakBestelling(Klant klant, IDFactory idFactory)
        {
            try
            {
                return new Bestelling(idFactory.MaakBestellingID(), DateTime.Now, klant);
            }
            catch (BestellingException ex)
            {
                throw new BestellingFactoryException("MaakBestelling", ex);
            }
        }
        public static Bestelling MaakBestelling(Klant klant, Dictionary<Product, int> producten, IDFactory idFactory)
        {
            try
            {
                return new Bestelling(idFactory.MaakBestellingID(), DateTime.Now, klant, producten);
            }
            catch (BestellingException ex)
            {
                throw new BestellingFactoryException("MaakBestelling", ex);
            }
        }
    }
}
