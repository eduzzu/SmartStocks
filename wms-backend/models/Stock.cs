using System.ComponentModel.DataAnnotations;

namespace SmartStocks.Models {
    public class Stock {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int StoreId { get; set; }
        public Store Store { get; set; } = null!;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative integer.")]
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}