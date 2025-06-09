using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TraceNest.Data;
using TraceNest.Helper.CloudinaryHelper;
using TraceNest.Hubs;
using TraceNest.Repository.AuthRepositories;
using TraceNest.Repository.CategoryRepositories;
using TraceNest.Repository.FoundRepositories;
using TraceNest.Repository.LostRepositories;
using TraceNest.Repository.MunicipalityRepositories;
using TraceNest.Repository.ProfileRepositories;
using TraceNest.Repository.UserRepositories;
using TraceNest.Services.AuthServices;
using TraceNest.Services.CategoryServices;
using TraceNest.Services.FoundServices;
using TraceNest.Services.LostServices;
using TraceNest.Services.MunicipalityServices;
using TraceNest.Services.ProfileServices;
using TraceNest.Services.UserServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(Options =>
	Options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

// Configure JWT Authentication
builder.Services.AddAuthentication("Bearer")
	.AddJwtBearer("Bearer", options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		};

		options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
		{
			OnMessageReceived = context =>
			{
				// Check for token in cookie first
				var token = context.Request.Cookies["AuthToken"];
				if (!string.IsNullOrEmpty(token))
				{
					context.Token = token;
					return Task.CompletedTask;
				}

				// For SignalR connections, check query string
				var path = context.HttpContext.Request.Path;
				if (path.StartsWithSegments("/chathub"))
				{
					var accessToken = context.Request.Query["access_token"];
					if (!string.IsNullOrEmpty(accessToken))
					{
						context.Token = accessToken;
					}
				}

				return Task.CompletedTask;
			},
			OnChallenge = context =>
			{
				context.HandleResponse();
				context.Response.StatusCode = 401;
				return Task.CompletedTask;
			}
		};
	});

// Register your services
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ILostRepository, LostRepository>();
builder.Services.AddScoped<ILostService, LostService>();
builder.Services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();
builder.Services.AddScoped<IMunicipalityService, MunicipalityService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<IFoundRepository, FoundRepository>();
builder.Services.AddScoped<IFoundService, FoundService>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Configure SignalR with proper authentication
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
builder.Services.AddSignalR(options =>
{
	options.EnableDetailedErrors = true; // Enable for debugging
	options.KeepAliveInterval = TimeSpan.FromSeconds(15);
	options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Authentication must come before authorization and hub mapping
app.UseAuthentication();
app.UseAuthorization();

// Map SignalR Hub
app.MapHub<ChatHub>("/chathub");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();