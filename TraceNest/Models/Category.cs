namespace TraceNest.Models
{
	public class Category
	{
		public Guid Id { get; set; } = new Guid();
		public string CategoryName { get; set; }
		public List<Lost> Losts { get; set; }
		public List<Found> Found { get; set; }
	}
}
