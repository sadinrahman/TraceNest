using System.ComponentModel.DataAnnotations;

namespace TraceNest.Dto
{
	public class UpdateUser
	{
		[Required]
		public string Username { get; set; }
		[EmailAddress]
		[Required]
		public string Email { get; set; }
	}
}
