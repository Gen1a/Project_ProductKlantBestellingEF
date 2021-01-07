using BusinessLayer.Exceptions;
using System;

namespace BusinessLayer.Models
{
    public class Product
    {
        #region Fields
        private string _name;   // geen auto-implemented property => backing field nodig
        private decimal _prijs;
        private long _productId;
        #endregion
        #region Constructors
        public Product(string naam)
        {
            Naam = naam;
        }
        public Product(string naam, decimal prijs) : this(naam)
        {
            Prijs = prijs;
        }
        public Product(string naam, decimal prijs, long id) : this(naam, prijs)
        {
            ProductId = id;
        }
        #endregion

        #region Properties
        // Unieke naam, wijzigbaar
        public string Naam
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ProductException("Naam van een product mag niet leeg zijn");
                _name = value;
            }
        }
        // Prijs in euro, wijzigbaar
        public decimal Prijs {
            get => _prijs;
            set
            {
                if (value < 0)
                    throw new ProductException("Prijs van product mag niet kleiner dan 0 zijn");
                _prijs = value;
                
            }
        }
        // Product Id nodig om referentie te behouden bij wijzigen van naam
        public long ProductId
        {
            get => _productId;
            set
            {
                if (value <= 0)
                    throw new ProductException("Id van product moet groter zijn dan 0");
                _productId = value;
            }
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"[Product] {ProductId}, {Naam}, €{Prijs}";
        }

        // Equals en GetHashCode overriden om Product instanties te kunnen vergelijken
        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   Naam == product.Naam;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Naam);
        }
        #endregion
    }
}
