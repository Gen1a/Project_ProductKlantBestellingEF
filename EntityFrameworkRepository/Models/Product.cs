using EntityFrameworkRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkRepository.Models
{
    public partial class Product : BaseEntity
    {
        [Required]
        public string Naam { get; set; }
        [Required]
        public decimal Prijs { get; set; }
        public int Valid { get; set; }
        // Collection navigation property which creates a Product foreign key in ProductBestellingen
        public virtual ICollection<Product_Bestelling> ProductBestellingen { get; set; }

        public Product()
        {
            // HashSetprovides high-performance set operations.
            // A set is a collection that contains no duplicate elements, 
            // and whose elements are in no particular order.
            ProductBestellingen = new HashSet<Product_Bestelling>();
        }
    }
}
