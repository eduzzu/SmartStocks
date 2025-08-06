using System.ComponentModel.DataAnnotations;

namespace SmartStocks.Models {
    public class User {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string firstName { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string lastdName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string email { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string password { get; set; } = string.Empty;
        [Required]
        [StringLength(10)]
        public UserRole role { get; set; } = UserRole.Employee;

        public int? StoreId { get; set; }
        public Store? Store { get; set; } = null!;

        public ICollection<UserStores> UserStores { get; set; } = new List<UserStores>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  
        public DateTime? UpdatedAt { get; set; }

    }
    public enum UserRole
{
    Employee,
    Admin,
    Manager,
}
}

