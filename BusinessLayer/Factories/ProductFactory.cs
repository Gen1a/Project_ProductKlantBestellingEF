using BusinessLayer.Exceptions;
using BusinessLayer.Models;

namespace BusinessLayer.Factories
{
    // static class so it's not needed to create an instance first
    public static class ProductFactory
    {
        public static Product MaakProduct(string naam, decimal prijs, IDFactory idFactory)
        {
            try
            {
                return new Product(naam.Trim(), prijs, idFactory.MaakProductID());
            }
            catch (ProductException ex)
            {
                throw new ProductFactoryException("MaakProduct", ex);
            }
        }
    }
}

