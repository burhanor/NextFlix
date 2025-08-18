using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class MovieTrailerConfiguration : IEntityTypeConfiguration<MovieTrailer>
	{
		public void Configure(EntityTypeBuilder<MovieTrailer> builder)
		{
			builder.HasIndex(mt => new { mt.MovieId, mt.TrailerLink })
				.IsUnique();
			builder.HasOne(mt => mt.Movie)
				.WithMany(m => m.MovieTrailers)
				.HasForeignKey(mt => mt.MovieId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
