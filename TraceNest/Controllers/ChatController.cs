//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Claims;
//using TraceNest.Data;
//using TraceNest.Dto;

//namespace TraceNest.Controllers
//{
//	public class ChatController : Controller
//	{
//		private readonly AppDbContext _context;
//		public ChatController(AppDbContext context)
//		{
//			_context = context;
//		}
//		public async Task<IActionResult> Index()
//		{
//			var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
//			Console.WriteLine(currentUserId);
//			// Get all unique users you've chatted with
//			var conversations = await _context.Messages
//				.Where(m => m.SenderId == currentUserId || m.ReceiverId == currentUserId)
//				.Select(m => new
//				{
//					UserId = m.SenderId == currentUserId ? m.ReceiverId : m.SenderId,
//					User = m.SenderId == currentUserId ? m.Receiver : m.Sender,
//					LastMessage = m.Message,
//					LastMessageTime = m.CreatedAt
//				})
//				.GroupBy(c => c.UserId)
//				.Select(g => new ConversationViewModel
//				{
//					UserId = g.Key,
//					UserName = g.First().User.Username,
//					LastMessage = g.OrderByDescending(x => x.LastMessageTime).First().LastMessage,
//					LastMessageTime = g.Max(x => x.LastMessageTime)
//				})
//				.OrderByDescending(c => c.LastMessageTime)
//				.ToListAsync();

//			return View(conversations);
//		}

//		// Show specific chat with a user
//		public async Task<IActionResult> Chat(Guid userId)
//		{
//			var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
//			var otherUser = await _context.users.FindAsync(userId);
//			Console.WriteLine(currentUserId + "Hi Chat");
//			if (otherUser == null)
//			{
//				return NotFound();
//			}

//			ViewBag.ReceiverId = userId;
//			ViewBag.ReceiverName = otherUser.Username;

//			return View("ChatWindow");
//		}

//		[HttpGet]
//		public async Task<IActionResult> GetMessages(Guid userId)
//		{
//			var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

//			var messages = await _context.Messages
//				.Where(m => (m.SenderId == currentUserId && m.ReceiverId == userId) ||
//							(m.SenderId == userId && m.ReceiverId == currentUserId))
//				.OrderBy(m => m.CreatedAt)
//				.Select(m => new
//				{
//					messageId = m.MessageId,
//					senderId = m.SenderId,
//					message = m.Message,
//					time = m.CreatedAt.ToString("hh:mm tt"),
//					isRead = m.Isread,
//					createdAt = m.CreatedAt
//				})
//				.ToListAsync();

//			return Ok(messages);
//		}
//	}
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TraceNest.Data;
using TraceNest.Dto;

namespace TraceNest.Controllers
{
	public class ChatController : Controller
	{
		private readonly AppDbContext _context;

		public ChatController(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

			var conversations = await _context.Messages
				.Where(m => m.SenderId == currentUserId || m.ReceiverId == currentUserId)
				.Select(m => new
				{
					UserId = m.SenderId == currentUserId ? m.ReceiverId : m.SenderId,
					User = m.SenderId == currentUserId ? m.Receiver : m.Sender,
					LastMessage = m.Message,
					LastMessageTime = m.CreatedAt
				})
				.GroupBy(c => c.UserId)
				.Select(g => new ConversationViewModel
				{
					UserId = g.Key,
					UserName = g.First().User.Username,
					LastMessage = g.OrderByDescending(x => x.LastMessageTime).First().LastMessage,
					LastMessageTime = g.Max(x => x.LastMessageTime),
					LastMessageTimeDisplay = g.Max(x => x.LastMessageTime).ToString("hh:mm tt")
				})
				.OrderByDescending(c => c.LastMessageTime)
				.ToListAsync();

			return View(conversations);
		}

		public async Task<IActionResult> Chat(Guid userId)
		{
			var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			var otherUser = await _context.users.FindAsync(userId);
			if (otherUser == null)
			{
				return NotFound();
			}

			ViewBag.ReceiverId = userId;
			ViewBag.ReceiverName = otherUser.Username;
			ViewBag.CurrentUserId = currentUserId;

			return View("ChatWindow");
		}

		[HttpGet]
		public async Task<IActionResult> GetMessages(Guid userId)
		{
			var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

			var messages = await _context.Messages
				.Where(m => (m.SenderId == currentUserId && m.ReceiverId == userId) ||
							(m.SenderId == userId && m.ReceiverId == currentUserId))
				.OrderBy(m => m.CreatedAt)
				.Select(m => new
				{
					messageId = m.MessageId,
					senderId = m.SenderId,
					message = m.Message,
					time = m.CreatedAt.ToString("hh:mm tt"),
					isRead = m.Isread,
					createdAt = m.CreatedAt
				})
				.ToListAsync();

			return Ok(messages);
		}
	}
}
