namespace TraceNest.Models
{
	public class Municipality
	{
		public Guid Id { get; set; } = new Guid();
		public string MunicipalityName { get; set; }
		public List<Lost> Lost { get; set; }
		public List<Found> Found { get; set; }
	}
}
