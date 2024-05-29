namespace BadmintonCenter.BusinessObject.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
        public ICollection<UserPackage> UserPackages { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; } = null!;
        public ICollection<Transaction> Transactions { get; set; } = null!;
    }
}
