
using System.ComponentModel.DataAnnotations;

namespace TraceNest.Dto
{
	public class LostAddDto
	{
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		
		public Guid? CategoryId { get; set; }
		
		public Guid? MunicipalityId { get; set; }
		[Required]
		public DateOnly LostDate { get; set; }
		public string CustomCategory { get; set; }
		public string customMunicipality { get; set; }
	}
}
