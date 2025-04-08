namespace TraceNest.Models
{
	public class Lost
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string Title { get; set; }
		public string Desciption { get; set; }
		public Guid CategoryId { get; set; }
		public Guid MunicipalityId { get; set; }
		public DateOnly LostDate { get; set; }
		public string ImageUrl { get; set; }
		public string Status { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
