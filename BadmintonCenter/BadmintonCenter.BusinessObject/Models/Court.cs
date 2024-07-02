using BadmintonCenter.Common.Enum.Status;
using System.ComponentModel.DataAnnotations;

namespace BadmintonCenter.BusinessObject.Models
{
    public class Court
    {
        public int CourtId { get; set; }
        [Required(ErrorMessage ="Court name is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Court name length must be between 1 and 50 characters")]
        public string CourtName { get; set; } = null!;
        [Required(ErrorMessage ="Description is required")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "Status is required")]
        public CourtStatus Status { get; set; }
        public ICollection<BookingDetail>? BookingDetails { get; set; }
    }
}
