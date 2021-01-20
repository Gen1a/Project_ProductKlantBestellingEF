﻿using BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BusinessLayerTests")]
namespace BusinessLayer.Models
{
    public class Klant : Observable
    {
        #region Fields
        private List<Bestelling> _bestellingen; // Lijst van bestellingen voor klant
        private string _naam;
        private string _adres;
        private long _klantId;
        #endregion

        #region Constructors
        internal Klant(string naam, string adres)
        {
            _bestellingen = new List<Bestelling>();
            Naam = naam;
            Adres = adres;
        }
        internal Klant(string naam, string adres, long klantId) : this(naam, adres)
        {
            KlantId = klantId;
        }
        internal Klant(string naam, string adres, long klantId, List<Bestelling> bestellingen) : this(naam, adres, klantId)
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
        public long KlantId
        {
            get => _klantId;
            set
            {
                if (value < 0)
                    throw new KlantException("Id van klant is invalide");
                _klantId = value;
            }
        }
        public string Naam {
            get => _naam;
            set
            {
                if (String.IsNullOrWhiteSpace(value)) throw new KlantException("Naam mag niet leeg zijn");
                _naam = value;
                NotifyPropertyChanged("Naam");
            }
        }
        public string Adres {
            get => _adres;
            set
            {
                if (String.IsNullOrWhiteSpace(value)) throw new KlantException("Adres mag niet leeg zijn");
                _adres = value;
                NotifyPropertyChanged("Adres");
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
            return $"[Klant] Id: {KlantId}, Naam: {Naam}, Adres: {Adres}, Bestellingen: {_bestellingen.Count}";
        }
        public void Show()
        {
            Console.WriteLine(this);
            foreach (Bestelling b in _bestellingen) Console.WriteLine($"    bestelling:{b}");
        }

        public override bool Equals(object obj)
        {
            return obj is Klant klant &&
                   KlantId == klant.KlantId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(KlantId);
        }


        #endregion
    }
}
