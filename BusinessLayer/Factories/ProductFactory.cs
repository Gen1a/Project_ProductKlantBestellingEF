using BusinessLayer.Exceptions;
using BusinessLayer.Models;

namespace BusinessLayer.Factories
{
    // static class so it's not needed to create an instance first
    public static class ProductFactory
    {
        public static Product MaakNieuwProduct(string naam, decimal prijs)
        {
            if (string.IsNullOrEmpty(naam)) throw new ProductFactoryException("ProductFactory: Naam van een product mag niet leeg zijn");
            if (prijs < 0) throw new ProductFactoryException("ProductFactory: Prijs van een product mag niet kleiner dan 0 zijn");

            return new Product(naam, prijs);
        }

        public static Product MaakNieuwProduct(string naam, decimal prijs, long id)
        {
            if (id < 0) throw new ProductFactoryException("ProductFactory: Id van product is invalide");
            Product p = MaakNieuwProduct(naam, prijs);
            p.ProductId = id;

            return p;
        }
    }
}

