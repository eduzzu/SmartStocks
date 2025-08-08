using System.ComponentModel.DataAnnotations;

namespace SmartStocks.Models {
    public class Store {
        public Guid Id {get; set;}
        [Required]
        [StringLength(255)]
        public string Name {get; set;} = string.Empty;
        [Required]
        [StringLength(255)]
        public string Location {get; set;} = string.Empty;

        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
}