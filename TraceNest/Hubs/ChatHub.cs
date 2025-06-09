using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TraceNest.Data;
using TraceNest.Models; // Adjust namespace according to your Message model

namespace TraceNest.Hubs
{
	public class ChatHub : Hub
	{
		private readonly AppDbContext _context;
		private static readonly Dictionary<string, string> UserConnections = new();

		public ChatHub(AppDbContext context)
		{
			_context = context;
		}

		public override async Task OnConnectedAsync()
		{
			var userId = Context.UserIdentifier;
			if (!string.IsNullOrEmpty(userId))
			{
				UserConnections[userId] = Context.ConnectionId;
				Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] User connecting: {userId}");
				Console.WriteLine($"User {userId} connected with ID: {Context.ConnectionId}. Total: {UserConnections.Count}");
			}
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			var userId = Context.UserIdentifier;
			if (!string.IsNullOrEmpty(userId))
			{
				UserConnections.Remove(userId);
				Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] User disconnecting: {userId}");
				Console.WriteLine($"User {userId} disconnected. Total: {UserConnections.Count}");
				if (exception != null)
				{
					Console.WriteLine($"Disconnect reason: {exception.Message}");
				}
			}
			await base.OnDisconnectedAsync(exception);
		}

		public async Task SendPrivateMessage(string receiverId, string message)
		{
			try
			{
				var senderId = Context.UserIdentifier;

				if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(receiverId) || string.IsNullOrEmpty(message))
				{
					Console.WriteLine("SendPrivateMessage: Invalid parameters");
					return;
				}

				Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Sending message from {senderId} to {receiverId}: {message}");

				// Save message to database
				var messageEntity = new Messages // Adjust according to your Message model
				{
					MessageId = Guid.NewGuid(),
					SenderId = Guid.Parse(senderId),
					ReceiverId = Guid.Parse(receiverId),
					Message = message,
					CreatedAt = DateTime.UtcNow,
					Isread = false
				};

				_context.Messages.Add(messageEntity);
				await _context.SaveChangesAsync();

				Console.WriteLine($"Message saved to database with ID: {messageEntity.MessageId}");

				// Send to receiver if they're online
				if (UserConnections.TryGetValue(receiverId, out var receiverConnectionId))
				{
					await Clients.Client(receiverConnectionId).SendAsync("ReceivePrivateMessage", senderId, message);
					Console.WriteLine($"Message sent to receiver's connection: {receiverConnectionId}");
				}
				else
				{
					Console.WriteLine($"Receiver {receiverId} is not online");
				}

				// Confirm to sender
				await Clients.Caller.SendAsync("MessageSent", message);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in SendPrivateMessage: {ex.Message}");
				Console.WriteLine($"Stack trace: {ex.StackTrace}");
				await Clients.Caller.SendAsync("MessageError", "Failed to send message");
			}
		}
	}
}