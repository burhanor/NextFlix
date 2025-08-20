using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NextFlix.Application.Abstraction.Interfaces.FileStorage;
using NextFlix.Application.Abstraction.Interfaces.RabbitMq;
using NextFlix.Application.Abstraction.Interfaces.Redis;
using NextFlix.Application.Abstraction.Interfaces.Token;
using NextFlix.Infrastructure.FileStorage;
using NextFlix.Infrastructure.RabbitMq;
using NextFlix.Infrastructure.Redis;
using NextFlix.Infrastructure.Token;
using System.Text;

namespace NextFlix.Infrastructure
{
	public static class Registration
	{
		public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			
			services.Configure<RabbitMqModel>(configuration.GetSection("RabbitMQ"));
			services.AddSingleton<IRabbitMqService, RabbitMqService>();

			services.Configure<RedisModel>(configuration.GetSection("Redis"));
			services.AddSingleton<IRedisService, RedisService>();


			services.Configure<TokenModel>(configuration.GetSection("JWT"));

			services.AddTransient<ITokenService, TokenService>();
			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
			{
				opt.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						var accessToken = context.Request.Cookies["accessToken"];
						if (!string.IsNullOrEmpty(accessToken))
						{
							context.Token = accessToken;
						}
						return Task.CompletedTask;
					}
				};
				opt.SaveToken = true;
				opt.TokenValidationParameters = new()
				{
					ValidateIssuerSigningKey = true,
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = false,
					ValidIssuer = configuration["JWT:Issuer"],
					ValidAudience = configuration["JWT:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"] ?? string.Empty)),
					ClockSkew = TimeSpan.Zero
				};
			});



			services.AddScoped<IFileStorageService, FileStorageService>();
			services.AddHostedService<RabbitMqHostedService>();

		}
	}
}
