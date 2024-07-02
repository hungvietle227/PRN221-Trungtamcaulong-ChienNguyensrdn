using System.ComponentModel.DataAnnotations;

namespace BadmintonCenter.BusinessObject.Models
{
    public class Package
    {
        public int PackageId { get; set; }
        [Required(ErrorMessage = "Package name is required")]
        public string PackageName { get; set; } = null!;
        public string? Description { get; set; }
        [Required(ErrorMessage ="Hours is required")]
        [Range(0, 10)]
        public int HourIncluded { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
        public ICollection<UserPackage>? UserPackages { get; set; }
    }
}
