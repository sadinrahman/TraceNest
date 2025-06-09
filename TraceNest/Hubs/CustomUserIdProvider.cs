using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace TraceNest.Hubs
{
	public class CustomUserIdProvider : IUserIdProvider
	{
			public string GetUserId(HubConnectionContext connection)
		{
			// Get user ID from JWT claims
			var userId = connection.User?.FindFirstValue(ClaimTypes.NameIdentifier);

			// Log for debugging
			Console.WriteLine($"CustomUserIdProvider - UserId: {userId}");
			Console.WriteLine($"CustomUserIdProvider - User Identity Name: {connection.User?.Identity?.Name}");
			Console.WriteLine($"CustomUserIdProvider - Is Authenticated: {connection.User?.Identity?.IsAuthenticated}");

			return userId;
		}
	}
	
}
