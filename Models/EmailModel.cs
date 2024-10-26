using System.ComponentModel.DataAnnotations;

namespace SupportCenter.Models
{
	public class EmailModel
	{
		[Required]
		[EmailAddress]
		public required string Email { get; set; }

		[Required]
		public required string Subject { get; set; }

		[Required]
		public required string Message { get; set; }
	}
}
