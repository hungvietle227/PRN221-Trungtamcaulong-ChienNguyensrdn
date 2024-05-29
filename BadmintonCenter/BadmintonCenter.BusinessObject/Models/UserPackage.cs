namespace BadmintonCenter.BusinessObject.Models
{
    public class UserPackage
    {
        public int UserId { get; set; }
        public int PackageId { get; set; }
        public int HourRemaining { get; set; }
        public int ValidInMonth { get; set; }
        public User User { get; set; } = null!;
        public Package Package { get; set; } = null!;
    }
}
