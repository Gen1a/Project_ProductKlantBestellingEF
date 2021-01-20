﻿using EntityFrameworkRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkRepository.Models
{
    public partial class Bestelling : BaseEntity
    {
        [Required]
        public long KlantId { get; set; }
        [Required]
        public DateTime Datum { get; set; }
        [Required]
        public bool Betaald { get; set; }
        [Required]
        public decimal Prijs { get; set; }
        public virtual Klant Klant { get; set; }
        // Collection navigation property which creates a Bestelling foreign key in ProductBestellingen
        public virtual ICollection<Product_Bestelling> ProductBestellingen { get; set; }

        public Bestelling()
        {
            // HashSetprovides high-performance set operations.
            // A set is a collection that contains no duplicate elements, 
            // and whose elements are in no particular order.
            ProductBestellingen = new HashSet<Product_Bestelling>();
        }
    }
}
