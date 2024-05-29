namespace BadmintonCenter.BusinessObject.Models
{
    public class Package
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; } = null!;
        public string? Description { get; set; }
        public int HourIncluded { get; set; }
        public double Price { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = null!;
        public ICollection<UserPackage> UserPackages { get; set; } = null!;
    }
}
