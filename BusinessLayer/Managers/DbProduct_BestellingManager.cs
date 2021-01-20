using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using EntityFrameworkRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Product_BestellingBL = BusinessLayer.Models.Product_Bestelling;
using Product_BestellingEF = EntityFrameworkRepository.Models.Product_Bestelling;

namespace BusinessLayer.Managers
{
    public class DbProduct_BestellingManager : IDbManager<Product_BestellingBL>
    {
        // Define repository instance which functions as a session which connects to the database
        private readonly EntityFrameworkRepository<Product_BestellingEF> _repository = new EntityFrameworkRepository<Product_BestellingEF>();
        // Needed to store the Bestelling objects which EF tracks
        private Dictionary<(long, long), (Product_BestellingEF, Product_BestellingBL)> _mappedObjects = new Dictionary<(long, long), (Product_BestellingEF, Product_BestellingBL)>();

        public DbProduct_BestellingManager()
        {
            HaalOp();
        }

        public IReadOnlyList<Product_BestellingBL> HaalOp()
        {
            var productBestellingen = new List<Product_BestellingBL>();
            _mappedObjects = _repository.List().ToDictionary(pb => (pb.ProductId, pb.BestellingId), pb =>
                (pb, new Product_BestellingBL(pb.ProductId, pb.BestellingId, pb.Aantal)));

            foreach (var dbItem in _mappedObjects.Values)
            {
                dbItem.Item2.Aantal = dbItem.Item1.Aantal;
                productBestellingen.Add(dbItem.Item2);
            }

            return productBestellingen;
        }

        public IReadOnlyList<Product_BestellingBL> HaalOp(Func<Product_BestellingBL, bool> predicate)
        {
            if (_mappedObjects.Count == 0)
                HaalOp();

            var productBestellingen = new List<Product_BestellingBL>();
            foreach (var dbItem in _mappedObjects.Values)
            {
                dbItem.Item2.Aantal = dbItem.Item1.Aantal;
                productBestellingen.Add(dbItem.Item2);
            }
            var selection = productBestellingen.Where<Product_BestellingBL>(predicate).ToList();

            return selection;
        }

        public long VoegToe(Product_BestellingBL productBestelling)
        {
            var pbEF = new Product_BestellingEF
            {
                ProductId = productBestelling.ProductId,
                BestellingId = productBestelling.BestellingId,
                Aantal = productBestelling.Aantal,
            };

            _repository.Create(pbEF); // Create in _repository calls SaveChanges() and returns ID (long)
            _mappedObjects[(productBestelling.ProductId, productBestelling.BestellingId)] = (pbEF, productBestelling);

            return pbEF.Id;
        }

        public void Verwijder(Product_BestellingBL productBestelling)
        {
            try
            {
                _repository.Delete(_mappedObjects[(productBestelling.ProductId, productBestelling.BestellingId)].Item1);    // Delete in _repository also calls SaveChanges();
                _mappedObjects.Remove((productBestelling.ProductId, productBestelling.BestellingId));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new DbProduct_BestellingManagerException("DbProduct_BestellingManager: Fout bij het verwijderen van Product_Bestellingen uit database");
            }
        }

        public Product_BestellingBL HaalOp(long id)
        {
            throw new NotImplementedException();
        }

        //public Product_BestellingBL HaalOp((long, long) tuple)
        //{
        //    // check if mappedObjects already contains Product_BestellingEF with this id
        //    if (_mappedObjects.ContainsKey(tuple)) return _mappedObjects[tuple].Item2;

        //    var pbEF = HaalOp(x => (x.ProductId == tuple.Item1) && (x.BestellingId == tuple.Item2)).FirstOrDefault();
        //    _mappedObjects[tuple] = (pbEF, new Product_BestellingBL(pbEF.ProductId, pbEF.BestellingId, pbEF.Aantal));

        //    return _mappedObjects[tuple].Item2;
        //}
    }
}
