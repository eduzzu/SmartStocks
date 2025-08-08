using System.ComponentModel.DataAnnotations;

namespace SmartStocks.Models {
    public class Order {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Guid StoreId { get; set; }
        public Store Store { get; set; } = null!;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderType OrderType { get; set; } = OrderType.Sale;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
    public enum OrderType
{
    Sale,
    Purchase
}

}
 

