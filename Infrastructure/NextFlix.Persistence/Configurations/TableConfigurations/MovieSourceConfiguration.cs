using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class MovieSourceConfiguration : IEntityTypeConfiguration<MovieSource>
	{
		public void Configure(EntityTypeBuilder<MovieSource> builder)
		{
			
			builder.HasIndex(ms => new { ms.MovieId, ms.SourceId })
				.IsUnique();
			builder.HasOne(ms => ms.Movie)
				.WithMany(m => m.MovieSources)
				.HasForeignKey(ms => ms.MovieId)
				.OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(ms => ms.Source)
				.WithMany(s => s.MovieSources)
				.HasForeignKey(ms => ms.SourceId)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}
