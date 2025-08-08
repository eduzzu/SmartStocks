using System.ComponentModel.DataAnnotations;

namespace SmartStocks.Models {
    public class Stock {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public Guid StoreId { get; set; }
        public Store Store { get; set; } = null!;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative integer.")]
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}