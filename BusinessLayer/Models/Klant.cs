using BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class Klant
    {
        #region Fields
        // Lijst van bestellingen voor klant
        private List<Bestelling> _bestellingen;
        private string _naam;
        private string _adres;
        private int _klantId;
        #endregion

        #region Constructors
        public Klant(string naam, string adres)
        {
            _bestellingen = new List<Bestelling>();
            Naam = naam;
            Adres = adres;
        }
        public Klant(string naam, string adres, int klantId) : this(naam, adres)
        {
            KlantId = klantId;
        }
        public Klant(string naam, string adres, int klantId, List<Bestelling> bestellingen) : this(naam, adres, klantId)
        {
            if (bestellingen == null) throw new KlantException("Collectie bestellingen mag niet leeg zijn.");
            _bestellingen = bestellingen;
            foreach (Bestelling b in _bestellingen) // indien Klant constructor wordt aangeroepen met bestellingen
            {
                b.Klant = this;
            }
        }
        #endregion

        #region Properties
        // Klant Id nodig om referentie te behouden bij wijzigen van vb. naam
        public int KlantId
        {
            get => _klantId;
            set
            {
                if (value <= 0)
                    throw new KlantException("Id van klant moet groter zijn dan 0");
                _klantId = value;
            }
        }
        public string Naam {
            get => _naam;
            set
            {
                if (String.IsNullOrWhiteSpace(value)) throw new KlantException("Naam mag niet leeg zijn");
                _naam = value;
            }
        }
        public string Adres {
            get => _adres;
            set
            {
                if (String.IsNullOrWhiteSpace(value)) throw new KlantException("Adres mag niet leeg zijn");
                _adres = value;
            }
        }

        public double KlantenKorting() { 
            if (_bestellingen.Count > 10) return 10.0;
            else if (_bestellingen.Count > 5) return 5.0;
            else return 0.0;
        }
        #endregion

        #region Methods
        public void VerwijderBestelling(Bestelling b)
        {
            if (b == null)
                throw new KlantException("Bestelling mag niet null zijn");
            if (!_bestellingen.Contains(b))
                throw new KlantException("Bestelling is niet aanwezig in bestellingen");
            else
            {
                if (b.Klant == this)    // indien de bestelling toegewezen is aan deze klant
                    b.VerwijderKlant();
                _bestellingen.Remove(b);
            }
        }

        public void VoegBestellingToe(Bestelling b)
        {
            if (b == null)
                throw new KlantException("Bestelling mag niet null zijn");
            if (_bestellingen.Contains(b))
                throw new KlantException("Bestelling is reeds aanwezig in bestellingen");
            else
            {
                _bestellingen.Add(b);
                if (b.Klant != this)
                    b.Klant = this;
            }
        }

        public bool HeeftBestelling(Bestelling b)
        {
            return _bestellingen.Contains(b);
        }
        public IReadOnlyList<Bestelling> GeefBestellingen()
        {
            return _bestellingen.AsReadOnly();
        }
        public override string ToString()
        {
            return $"[Klant] {KlantId}, {Naam}, {Adres}, {_bestellingen.Count}";
        }
        public void Show()
        {
            Console.WriteLine(this);
            foreach (Bestelling b in _bestellingen) Console.WriteLine($"    bestelling:{b}");
        }
        #endregion
    }
}
