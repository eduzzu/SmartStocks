using System.ComponentModel.DataAnnotations;

namespace SmartStocks.Models {
    public class Category {
        public Guid Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}