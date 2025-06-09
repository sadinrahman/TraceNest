namespace TraceNest.Dto
{
	public class ConversationViewModel
	{
		public Guid UserId { get; set; }
		public string UserName { get; set; }
		public string LastMessage { get; set; }
		public DateTime LastMessageTime { get; set; }
		public string LastMessageTimeDisplay { get; set; }
	}
}
