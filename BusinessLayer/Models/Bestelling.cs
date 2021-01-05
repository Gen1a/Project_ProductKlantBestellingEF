using BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class Bestelling
    {
        #region Fields
        private Dictionary<Product, int> _producten;
        private int _bestellingId;
        private bool _betaald;
        private Klant _klant;
        private DateTime _datum;
        #endregion

        #region Constructors
        public Bestelling(int id, DateTime datum)
        {
            _producten = new Dictionary<Product, int>();
            Betaald = false;
            BestellingId = id;
            Datum = datum;
        }
        public Bestelling(int id, DateTime datum, Klant klant) : this(id, datum)
        {
            Klant = klant;
        }
        public Bestelling(int id, DateTime datum, Klant klant, Dictionary<Product, int> producten) : this(id, datum, klant)
        {
            if (producten == null) throw new BestellingException("Collectie producten mag niet leeg zijn");
            _producten = producten;
        }
        #endregion

        #region Properties
        public int BestellingId     // unique identifier
        {   
            get => _bestellingId;
            set {
                if (value <= 0)
                    throw new BestellingException("BestellingsId moet groter zijn dan 0");
                _bestellingId = value;
            }
        }
        public Klant Klant {
            get => _klant;
            set
            {
                if (value == null)
                    throw new BestellingException("Klant mag niet null zijn");
                if (value == Klant)
                    throw new BestellingException("Klant is identiek");
                if (Klant != null)  // indien reeds klant toegewezen aan bestelling
                    if (Klant.HeeftBestelling(this))    
                        Klant.VerwijderBestelling(this);    // bestelling verwijderen bij oorspronkelijke klant
                if (!value.HeeftBestelling(this))   // indien bestelling nog niet aanwezig bij nieuwe klant
                    value.VoegBestellingToe(this);
                _klant = value;
            }
        }
        public DateTime Datum {
            get => _datum;
            set
            {
                if (value == null)
                    throw new BestellingException("Datum van bestelling mag niet null zijn");
                _datum = value;
            }
        }
        public decimal PrijsBetaald { get; private set; }

        public bool Betaald {
            get => _betaald;
            set
            {
                _betaald = value;
                if (value)
                    PrijsBetaald = BerekenKostprijs();
            }
        }
        #endregion

        #region Methods
        public decimal BerekenKostprijs()
        {
            double korting;
            decimal prijs = 0.0m;
            if (_klant == null) 
                korting = 0.0;
            else 
                korting = _klant.KlantenKorting();
            foreach (KeyValuePair<Product, int> product in _producten)
            {
                prijs += product.Key.Prijs * product.Value * (decimal)(100.0 - korting) / 100.0m;
            }
            return prijs;
        }

        public void VoegProductToe(Product product, int aantal)
        {
            if (aantal <= 0)
                throw new BestellingException("Aantal producten moet groter dan 0 zijn");
            if (product == null)
                throw new BestellingException("Een product mag niet null zijn");
            if (_producten.ContainsKey(product))    // indien product reeds in collectie
                _producten[product] += aantal;
            else
                _producten.Add(product, aantal);
        }

        public void VerwijderProduct(Product product, int aantal)
        {
            if (aantal <= 0)
                throw new BestellingException("Aantal producten moet groter dan 0 zijn");
            if (product == null)
                throw new BestellingException("Een product mag niet null zijn");
            if (!_producten.ContainsKey(product))
                throw new BestellingException("Product is niet aanwezig in bestelling");
            else
            {
                if (_producten[product] < aantal)
                    throw new BestellingException("Aantal producten in bestelling is kleiner dan aantal");
                else if (_producten[product] == aantal)
                    _producten.Remove(product);
                else
                    _producten[product] -= aantal;
            }
        }

        public void VerwijderKlant()
        {
            _klant = null;
        }
        public IReadOnlyDictionary<Product, int> GeefProducten()
        {
            return _producten;
        }
        public override string ToString()
        {
            return $"[Bestelling] {BestellingId}, {Betaald}, €{PrijsBetaald}, {Datum}, {Klant}, {_producten.Count}";
        }

        public override bool Equals(object obj)
        {
            return obj is Bestelling bestelling &&
                   BestellingId == bestelling.BestellingId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BestellingId);
        }
        public void Show()
        {
            Console.WriteLine(this);
            foreach (KeyValuePair<Product, int> product in _producten)
                Console.WriteLine($"    product:{product.Key},{product.Value}");
        }
        #endregion
    }
}
