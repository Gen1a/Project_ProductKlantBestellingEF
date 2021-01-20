using BusinessLayer.Exceptions;
using BusinessLayer.Factories;
using BusinessLayer.Interfaces;
using EntityFrameworkRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using KlantBL = BusinessLayer.Models.Klant;
using KlantEF = EntityFrameworkRepository.Models.Klant;

namespace BusinessLayer.Managers
{
    public class DbKlantManager : IDbManager<KlantBL>
    {
        // Define repository instance which functions as a session which connects to the database
        private readonly EntityFrameworkRepository<KlantEF> _repository = new EntityFrameworkRepository<KlantEF>();
        // Needed to store the Klant objects which EF tracks
        private Dictionary<long, (KlantEF, KlantBL)> _mappedObjects = new Dictionary<long, (KlantEF, KlantBL)>();


        public IReadOnlyList<KlantBL> HaalOp()
        {
            var klanten = new List<KlantBL>();
            _mappedObjects = _repository.List().ToDictionary(c => c.Id, c => 
                (c, KlantFactory.MaakNieuweKlant(c.Naam, c.Adres, c.Id)));

            foreach(var dbItem in _mappedObjects.Values)
            {
                klanten.Add(dbItem.Item2);
            }

            return klanten;
        }

        public IReadOnlyList<KlantBL> HaalOp(Func<KlantBL, bool> predicate)
        {
            var klanten = new List<KlantBL>();
            foreach (var dbItem in _mappedObjects.Values)
            {
                klanten.Add(dbItem.Item2);
            }
            var selection = klanten.Where<KlantBL>(predicate).ToList();

            return (IReadOnlyList<KlantBL>)selection;
        }

        public KlantBL HaalOp(long id)
        {
            // check if mappedObjects already contains KlantEF with this id
            if (_mappedObjects.ContainsKey(id)) return _mappedObjects[id].Item2;

            var klantEF = _repository.GetById(id);
            _mappedObjects[id] = (klantEF, KlantFactory.MaakNieuweKlant(klantEF.Naam, klantEF.Adres, klantEF.Id));

            return _mappedObjects[id].Item2;
        }

        public long VoegToe(KlantBL klant)
        {
            var newKlantEF = new KlantEF { Naam = klant.Naam, Adres = klant.Adres };
            klant.KlantId = newKlantEF.Id = _repository.Create(newKlantEF);     // Create in _repository calls SaveChanges() and returns ID (long)
            _mappedObjects[newKlantEF.Id] = (newKlantEF, klant);

            return newKlantEF.Id;
        }

        public void Verwijder(KlantBL klant)
        {
            try
            {
                _repository.Delete(_mappedObjects[klant.KlantId].Item1);    // Delete in _repository also calls SaveChanges();
                _mappedObjects.Remove(klant.KlantId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new DbKlantManagerException("DbKlantManager: Fout bij het verwijderen van Klant uit database");
            }
        }
    }
}
