using Microsoft.EntityFrameworkCore;
using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Context
{
	public class MovieDbContext(DbContextOptions<MovieDbContext> options) : DbContext(options)
	{
		#region Tables

		public DbSet<Cast> Casts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Channel> Channels { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Log> Logs { get; set; }
		public DbSet<Login> Logins { get; set; }
		public DbSet<Movie> Movies { get; set; }
		public DbSet<MovieCast> MovieCasts { get; set; }
		public DbSet<MovieCategory> MovieCategories { get; set; }
		public DbSet<MovieChannel> MovieChannels { get; set; }
		public DbSet<MovieCountry> MovieCountries { get; set; }
		public DbSet<MovieLike> MovieLikes { get; set; }
		public DbSet<MovieSource> MovieSources { get; set; }
		public DbSet<MovieTag> MovieTags { get; set; }
		public DbSet<MovieTrailer> MovieTrailers { get; set; }
		public DbSet<MovieView> MovieViews { get; set; }
		public DbSet<Source> Sources { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<User> Users { get; set; }

		#endregion


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieDbContext).Assembly);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}
	}
}
