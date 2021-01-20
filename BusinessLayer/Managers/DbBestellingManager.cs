using BusinessLayer.Exceptions;
using BusinessLayer.Factories;
using BusinessLayer.Interfaces;
using EntityFrameworkRepository;
using EntityFrameworkRepository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BestellingBL = BusinessLayer.Models.Bestelling;
using BestellingEF = EntityFrameworkRepository.Models.Bestelling;


namespace BusinessLayer.Managers
{
    public class DbBestellingManager : IDbManager<BestellingBL>
    {
        // Define repository instance which functions as a session which connects to the database
        private readonly EntityFrameworkRepository<BestellingEF> _repository = new EntityFrameworkRepository<BestellingEF>();
        // Needed to store the Bestelling objects which EF tracks
        private Dictionary<long, (BestellingEF, BestellingBL)> _mappedObjects;
        DbProduct_BestellingManager _pbManager = new DbProduct_BestellingManager();

        public IReadOnlyList<BestellingBL> HaalOp()
        {
            var bestellingen = new List<BestellingBL>();
            DbKlantManager klantManager = new DbKlantManager();
            _mappedObjects = _repository.List().ToDictionary(b => b.Id, b => 
                (b, BestellingFactory.MaakNieuweBestelling(b.Datum, klantManager.HaalOp(b.KlantId), b.Id)));

            foreach(var dbItem in _mappedObjects.Values)
            {
                dbItem.Item2.PrijsBetaald = dbItem.Item1.Prijs; // Waarom deze lijn nodig??
                dbItem.Item2.Betaald = dbItem.Item1.Betaald;
                bestellingen.Add(dbItem.Item2);
            }

            return bestellingen;
        }

        public IReadOnlyList<BestellingBL> HaalOp(Func<BestellingBL, bool> predicate)
        {
            var bestellingen = new List<BestellingBL>();
            foreach (var dbItem in _mappedObjects.Values)
            {
                dbItem.Item2.PrijsBetaald = dbItem.Item1.Prijs; // Waarom deze lijn nodig??
                dbItem.Item2.Betaald = dbItem.Item1.Betaald;
                bestellingen.Add(dbItem.Item2);
            }
            var selection = bestellingen.Where<BestellingBL>(predicate).ToList();

            return (IReadOnlyList<BestellingBL>)selection;
        }

        public BestellingBL HaalOp(long id)
        {
            // check if mappedObjects already contains BestellingEF with this id
            if (_mappedObjects.ContainsKey(id)) return _mappedObjects[id].Item2;

            var bestellingEF = _repository.GetById(id);
            _mappedObjects[id] = (bestellingEF, BestellingFactory.MaakNieuweBestelling(
                bestellingEF.Datum, KlantFactory.MaakNieuweKlant(bestellingEF.Klant.Naam, bestellingEF.Klant.Adres, bestellingEF.Klant.Id), bestellingEF.Id));

            return _mappedObjects[id].Item2;
        }

        public long VoegToe(BestellingBL bestelling)
        {
            var newBestellingEF = new BestellingEF
            {
                KlantId = bestelling.Klant.KlantId,
                Datum = bestelling.Datum,
                Betaald = bestelling.Betaald,
                Prijs = bestelling.PrijsBetaald,
            };

            bestelling.BestellingId = newBestellingEF.Id = _repository.Create(newBestellingEF); // Create in _repository calls SaveChanges() and returns ID (long)
            _mappedObjects[newBestellingEF.Id] = (newBestellingEF, bestelling);

            // Create related Product_Bestelling Lines
            foreach (var kvp in bestelling.GeefProducten())
            {
                _pbManager.VoegToe(new BusinessLayer.Models.Product_Bestelling(kvp.Key.ProductId, newBestellingEF.Id, kvp.Value));
            }

            return newBestellingEF.Id;
        }

        public void Verwijder(BestellingBL bestelling)
        {
            try
            {
                _repository.Delete(_mappedObjects[bestelling.BestellingId].Item1);    // Delete in _repository also calls SaveChanges();
                _mappedObjects.Remove(bestelling.BestellingId);
                foreach (var kvp in bestelling.GeefProducten())
                {
                    _pbManager.Verwijder(new BusinessLayer.Models.Product_Bestelling(kvp.Key.ProductId, bestelling.BestellingId, kvp.Value));
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new DbBestellingManagerException("DbBestellingManager: Fout bij het verwijderen van Bestelling uit database");
            }
        }

        public void Verwijder(BestellingBL bestelling, bool isUpdate)
        {
            try
            {
                _repository.Delete(_mappedObjects[bestelling.BestellingId].Item1);    // Delete in _repository also calls SaveChanges();
                _mappedObjects.Remove(bestelling.BestellingId);
                if (!isUpdate)
                {
                    foreach (var kvp in bestelling.GeefProducten())
                    {
                        _pbManager.Verwijder(new BusinessLayer.Models.Product_Bestelling(kvp.Key.ProductId, bestelling.BestellingId, kvp.Value));
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new DbBestellingManagerException("DbBestellingManager: Fout bij het verwijderen van Bestelling uit database");
            }
        }

        public void Update(BestellingBL bestelling)
        {
            try
            {
                _repository.Update(_mappedObjects[bestelling.BestellingId].Item1);
                _mappedObjects.Remove(bestelling.BestellingId);
                foreach (var kvp in bestelling.GeefProducten())
                {
                    _pbManager.Verwijder(new BusinessLayer.Models.Product_Bestelling(kvp.Key.ProductId, bestelling.BestellingId, kvp.Value));
                }

                var newBestellingEF = new BestellingEF
                {
                    KlantId = bestelling.Klant.KlantId,
                    Datum = bestelling.Datum,
                    Betaald = bestelling.Betaald,
                    Prijs = bestelling.PrijsBetaald,
                };

                _mappedObjects[bestelling.BestellingId] = (newBestellingEF, bestelling);

                // Create related Product_Bestelling Lines
                foreach (var kvp in bestelling.GeefProducten())
                {
                    _pbManager.VoegToe(new BusinessLayer.Models.Product_Bestelling(kvp.Key.ProductId, bestelling.BestellingId, kvp.Value));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new DbBestellingManagerException("DbBestellingManager: Fout bij het updaten van Bestelling in database");
            }
        }
    }
}
