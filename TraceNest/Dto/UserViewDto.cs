using System.ComponentModel.DataAnnotations;

namespace TraceNest.Dto
{
	public class UserViewDto
	{
		public Guid Id { get; set; } 
		
		public string Username { get; set; }
		
		public string Email { get; set; }
		public bool IsActive { get; set; } 
	}
}
