namespace SmartStocks.Models {
    public class UserStores 
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int StoreId { get; set; }
    public Store Store { get; set; } = null!;
}

}
