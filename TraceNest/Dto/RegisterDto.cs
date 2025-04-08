using System.ComponentModel.DataAnnotations;
namespace TraceNest.Dto
{
	public class RegisterDto
	{
		[Required(ErrorMessage = "Username is required.")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email address.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		[MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
		[RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is required.")]
		[Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}
}