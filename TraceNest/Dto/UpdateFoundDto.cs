namespace TraceNest.Dto
{
	public class UpdateFoundDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Guid? Category { get; set; }
		public Guid? Municipality { get; set; }
		public DateOnly FoundDate { get; set; }

		public string Status { get; set; }
	}
}
