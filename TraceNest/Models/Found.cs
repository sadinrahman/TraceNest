namespace TraceNest.Models
{
	public class Found
	{
		public Guid Id { get; set; } = new Guid();
		public Guid UserId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Guid? CategoryId { get; set; }
		public Guid? MunicipalityId { get; set; }
		public DateOnly FoundDate { get; set; }
		public string ImageUrl { get; set; }
		public string Status { get; set; } = "Pending";
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public virtual User User { get; set; }
		public virtual Municipality Municipality { get; set; }
		public virtual Category Category { get; set; }
	}
}
