using BusinessLayer.Exceptions;
using BusinessLayer.Factories;
using BusinessLayer.Interfaces;
using EntityFrameworkRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ProductBL = BusinessLayer.Models.Product;
using ProductEF = EntityFrameworkRepository.Models.Product;

namespace BusinessLayer.Managers
{
    public class DbProductManager : IDbManager<ProductBL>
    {
        // Define repository instance which functions as a session which connects to the database
        private readonly EntityFrameworkRepository<ProductEF> _repository = new EntityFrameworkRepository<ProductEF>();
        // Needed to store the Product objects which EF tracks
        private Dictionary<long, (ProductEF, ProductBL)> _mappedObjects;

        public IReadOnlyList<ProductBL> HaalOp()
        {
            var producten = new List<ProductBL>();
            _mappedObjects = _repository.List().ToDictionary(p => p.Id, p =>
                (p, ProductFactory.MaakNieuwProduct(p.Naam, p.Prijs, p.Id)));

            foreach (var dbItem in _mappedObjects.Values)
            {
                producten.Add(dbItem.Item2);
            }

            return producten;
        }

        public IReadOnlyList<ProductBL> HaalOp(Func<ProductBL, bool> predicate)
        {
            var producten = new List<ProductBL>();
            foreach (var dbItem in _mappedObjects.Values)
            {
                producten.Add(dbItem.Item2);
            }
            var selection = producten.Where<ProductBL>(predicate).ToList();

            return (IReadOnlyList<ProductBL>)selection;
        }

        public ProductBL HaalOp(long id)
        {
            // check if mappedObjects already contains KlantEF with this id
            if (_mappedObjects.ContainsKey(id)) return _mappedObjects[id].Item2;

            var productEF = _repository.GetById(id);
            _mappedObjects[id] = (productEF, ProductFactory.MaakNieuwProduct(productEF.Naam, productEF.Prijs, productEF.Id));

            return _mappedObjects[id].Item2;
        }

        public long VoegToe(ProductBL product)
        {
            var newProductEF = new ProductEF { Naam = product.Naam, Prijs = product.Prijs };
            product.ProductId = newProductEF.Id = _repository.Create(newProductEF);     // Create in _repository calls SaveChanges() and returns ID (long)
            _mappedObjects[newProductEF.Id] = (newProductEF, product);

            return newProductEF.Id;
        }

        public void Verwijder(ProductBL product)
        {
            try
            {
                _repository.Delete(_mappedObjects[product.ProductId].Item1);    // Delete in _repository also calls SaveChanges();
                _mappedObjects.Remove(product.ProductId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new DbProductManagerException("DbProductManager: Fout bij het verwijderen van Product uit database");
            }
        }
    }
}
