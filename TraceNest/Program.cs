using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TraceNest.Data;
using TraceNest.Helper.CloudinaryHelper;
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
				// Read token from cookie named "AuthToken"
				var token = context.Request.Cookies["AuthToken"];
				if (!string.IsNullOrEmpty(token))
				{
					context.Token = token;
				}
				return Task.CompletedTask;
			}
		};
	});





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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
