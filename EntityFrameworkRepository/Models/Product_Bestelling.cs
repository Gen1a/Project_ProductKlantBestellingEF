using EntityFrameworkRepository.Interfaces;

namespace EntityFrameworkRepository.Models
{
    public partial class Product_Bestelling : BaseEntity
    {
        public long ProductId { get; set; }
        public long BestellingId { get; set; }
        public int Aantal { get; set; }

        // Below not necessary as Product & Bestelling implement an ICollection?
        public virtual Bestelling Bestelling { get; set; }
        public virtual Product Product { get; set; }
    }
}
