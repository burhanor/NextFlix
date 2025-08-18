using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextFlix.Application.Abstraction.Interfaces.Repositories;
using NextFlix.Application.Abstraction.Interfaces.Uow;
using NextFlix.Persistence.Context;
using NextFlix.Persistence.Repositories;
using NextFlix.Persistence.UnitOfWork;

namespace NextFlix.Persistence
{
	public static class Registration
	{
		public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<MovieDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("MSSQLConnection"));
			});

			services.AddScoped<IUow, Uow>();
			services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
			services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));


		}
	}
}
