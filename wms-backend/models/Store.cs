using System.ComponentModel.DataAnnotations;

namespace SmartStocks.Models {
    public class Store {
        public int Id {get; set;}
        [Required]
        [StringLength(100)]
        public string Name {get; set;} = string.Empty;
        [Required]
        [StringLength(100)]
        public string Location {get; set;} = string.Empty;

        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
}