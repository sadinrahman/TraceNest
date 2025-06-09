namespace TraceNest.Dto
{
	public class FoundItemDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public DateOnly FoundDate { get; set; }
		public string Municipality { get; set; }
		public string Status { get; set; } 
		public string Category { get; set; }
		public Guid userid { get; set; }
	}
}
