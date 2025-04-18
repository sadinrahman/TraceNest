﻿namespace TraceNest.Dto
{
	public class LostItemDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public DateOnly LostDate { get; set; }
		public string Municipality { get; set; }
		public string Status { get; set; }
		public string Category { get; set; }
	}
}
