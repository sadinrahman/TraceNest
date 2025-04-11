
using System.ComponentModel.DataAnnotations;

namespace TraceNest.Dto
{
	public class LostAddDto
	{
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public Guid CategoryId { get; set; }
		[Required]
		public Guid MunicipalityId { get; set; }
		[Required]
		public DateOnly LostDate { get; set; }
		
	}
}
