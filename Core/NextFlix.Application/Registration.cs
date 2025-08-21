using Microsoft.Extensions.DependencyInjection;
using NextFlix.Application.Helpers;
using NextFlix.Application.Interfaces;
using System.Reflection;

namespace NextFlix.Application
{
	public static class Registration
	{
		public static void AddApplication(this IServiceCollection services)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
			services.AddAutoMapper(cfg => cfg.AddMaps(assembly)); 
			services.AddScoped<IMovieHelper,MovieHelper>();
		}
	}
}
