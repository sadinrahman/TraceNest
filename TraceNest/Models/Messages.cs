using System.ComponentModel.DataAnnotations;

namespace TraceNest.Models
{
	public class Messages
	{
		[Key]
		public Guid MessageId { get; set; }
		public Guid SenderId { get; set; }
		public Guid ReceiverId { get; set; }
		public string Message { get; set; }
		public bool Isread { get; set; }=false;
		public DateTime CreatedAt { get; set; }=DateTime.Now;
		public User Sender { get; set; }
		public User Receiver { get; set; }
	}
}
