using System.ComponentModel.DataAnnotations;

namespace TraceNest.Models
{
	public class User
	{
		public Guid Id { get; set; } = new Guid();
		[Required]
		public string Username { get; set; }
		[EmailAddress]
		[Required]
		public string Email { get; set; }
		[Required]
		[MinLength(6, ErrorMessage = "Password must be above 6 characters")]
		[RegularExpression(@"^(?=.[A-Za-z])(?=.\d)(?=.[@$!%?&])[A-Za-z\d@$!%*?&]{8,}$",
		 ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
		public string Password { get; set; }
		public string Role { get; set; } = "user";
		public bool IsActive { get; set; } = true;
	}
}
