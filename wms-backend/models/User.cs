using System.ComponentModel.DataAnnotations;

namespace SmartStocks.Models {
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(255)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(255, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [StringLength(255)]
        public UserRole Role { get; set; } = UserRole.Employee;

        public Guid? StoreId { get; set; }
        public Store? Store { get; set; } = null!;

        public ICollection<User> UserStores { get; set; } = new List<User>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
    public enum UserRole
{
    Employee,
    Admin,
    Manager,
}
}

