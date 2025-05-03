namespace TraceNest.Dto
{
	public class ConversationDto
	{
		public Guid UserId { get; set; }
		public string UserName { get; set; }
		public string LastMessage { get; set; }
		public DateTime LastMessageTime { get; set; }
		public bool IsRead { get; set; }

	}
}
