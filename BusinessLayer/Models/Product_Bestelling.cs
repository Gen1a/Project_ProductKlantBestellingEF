using BusinessLayer.Exceptions;
using System;


namespace BusinessLayer.Models
{
    public class Product_Bestelling
    {
        private long _productId;
        private long _bestellingId;
        private int _aantal;

        internal Product_Bestelling(long productId, long bestellingId, int aantal)
        {
            ProductId = productId;
            BestellingId = bestellingId;
            Aantal = aantal;
        }

        public long ProductId
        {
            get => _productId;
            set
            {
                if (value < 0)
                {
                    throw new Product_BestellingException("ProductId mag niet kleiner dan 0 zijn");
                }
                _productId = value;
            }
        }

        public long BestellingId
        {
            get => _bestellingId;
            set
            {
                if (value < 0)
                {
                    throw new Product_BestellingException("BestellingId mag niet kleiner dan 0 zijn");
                }
                _bestellingId = value;
            }
        }

        public int Aantal
        {
            get => _aantal;
            set
            {
                if (value <= 0)
                {
                    throw new Product_BestellingException("Aantal moet groter dan 0 zijn");
                }
                _aantal = value;
            }
        }
    }
}
