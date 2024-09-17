
using Market.Data;
using Market.Entities;
using Market.Helper;
using Market.Repositories;
using Market.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Market
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddIdentity<application_user, IdentityRole>().AddEntityFrameworkStores<MarketContext>();
			builder.Services.AddDbContext<MarketContext>(options =>
			options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
			ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));
			builder.Services.AddControllers();

			builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
			builder.Services.AddSingleton(sp =>
				sp.GetRequiredService<IOptions<JWT>>().Value);

			builder.Services.AddScoped<IAuthService, Authservice>();

			builder.Services.AddAuthentication(op =>
			{
				op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(o =>
			{
				o.RequireHttpsMetadata = false;
				o.SaveToken = false;
				o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidIssuer = builder.Configuration["JWT:Issuer"],
					ValidAudience = builder.Configuration["JWT:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
				};
			});

			// add cores
			builder.Services.AddCors(options =>
			{
				options.AddDefaultPolicy(
					policy =>
					{
						policy.AllowAnyOrigin()
								.AllowAnyMethod()
								.AllowAnyHeader();
					});
			});
			builder.Services.AddAuthentication(
		options =>
		{
			options.DefaultScheme = IdentityConstants.ApplicationScheme;
			options.DefaultSignInScheme = IdentityConstants.ExternalScheme;

		});
			builder.Services.AddAuthorization(options =>
			{

				options.AddPolicy("Customer", policy =>
				{
					policy.RequireAuthenticatedUser();
				});

			});
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddScoped<IProductService, ProductRepository>();
			builder.Services.AddScoped<ICategoryService, CategoryRepository>();
			/*builder.Services.Configure<IdentityOptions>(options =>
			{
				options.User.RequireUniqueEmail = false; 
				options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
			});*/

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
