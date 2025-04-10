
using Contact_management.Contexts;
using Contact_management.Models;
using Contact_management.Repository;
using Contact_management.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace Contact_management
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			 
			#region  DBConnection
               builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			#endregion

			#region Identity
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			builder.Services.Configure<IdentityOptions>(options =>
			{
				//options.Password.RequireDigit = true;
				//options.Password.RequiredLength = 8;
				//options.Password.RequireLowercase = true;
				//options.Password.RequireUppercase = true;
				//options.Password.RequireNonAlphanumeric = true;
				//options.Password.RequiredUniqueChars = 1;

				// Lockout to avoid attack Brute Force
				options.Lockout.MaxFailedAccessAttempts = 5;      
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(120);    
				options.Lockout.AllowedForNewUsers = true;      
			});
			#endregion

			#region JWTSettings
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key)
				};
			});
			#endregion
			
			#region Swagger Setting
			builder.Services.AddSwaggerGen(swagger =>
			{
				// Generate the Swagger documentation with metadata for the API
				swagger.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "ASP.NET 8 Web API",
					Description = "Contact Managment"
				});

				// Add Security Definition for JWT Authentication
				swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "Enter 'Bearer' followed by a space and your token.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
				});

				// Add Security Requirement for Swagger UI to use the Bearer token
				swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
		{
			{
				new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				},
				Array.Empty<string>()
			}
		});
			});
			#endregion

			builder.Services.AddAuthorization();
			builder.Services.AddScoped<JWTTokenService>();
			builder.Services.AddScoped<IContactRepository, ContactRepository>();
			builder.Services.AddScoped<IContactService, ContactService>();
			builder.Services.AddScoped<ITokenService, JWTTokenService>();
			builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
		//	if (app.Environment.IsDevelopment())
		//	{
				app.UseSwagger();
				app.UseSwaggerUI();
	//		}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
