using EntityFrameworkRepository.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkRepository.Models
{
    public partial class Klant : BaseEntity
    {
        [Required]
        public string Naam { get; set; }
        [Required]
        public string Adres { get; set; }
        // Collection navigation property which creates a Klant foreign key in Bestellingen
        public virtual ICollection<Bestelling> Bestellingen { get; set; }

        public Klant()
        {
            // HashSetprovides high-performance set operations.
            // A set is a collection that contains no duplicate elements, 
            // and whose elements are in no particular order.
            Bestellingen = new HashSet<Bestelling>();
        }
    }
}
