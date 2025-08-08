namespace SmartStocks.Models
{
    public class Register
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.Employee;

    }
}