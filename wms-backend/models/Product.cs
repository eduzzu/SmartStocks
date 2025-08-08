using System.ComponentModel.DataAnnotations;

namespace SmartStocks.Models {
    public class Product {
        public Guid Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(255)]
        public string SKU { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(255)]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
}