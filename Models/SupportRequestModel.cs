using System.ComponentModel.DataAnnotations;

namespace SupportCenter.Models
{
    public class SupportRequest
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Subject { get; set; }

        [Required]
        public required string Message { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [DataType(DataType.Date)]
        public DateTime? RequestDate { get; set; }

        [StringLength(50)]
        public string? Priority { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(100)]
        public string? AssignedTo { get; set; }

        [StringLength(500)]
        public string? Resolution { get; set; }

        [StringLength(100)]
        public string? Category { get; set; } 
    }
}
