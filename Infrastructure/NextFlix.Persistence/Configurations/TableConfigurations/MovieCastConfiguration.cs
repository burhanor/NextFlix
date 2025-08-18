using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class MovieCastConfiguration : IEntityTypeConfiguration<MovieCast>
	{
		public void Configure(EntityTypeBuilder<MovieCast> builder)
		{
			builder.HasIndex(mc => new { mc.MovieId, mc.CastId })
				.IsUnique();
			builder.HasOne(mc => mc.Movie)
				.WithMany(m => m.MovieCasts)
				.HasForeignKey(mc => mc.MovieId)
				.OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(mc => mc.Cast)
				.WithMany(c => c.MovieCasts)
				.HasForeignKey(mc => mc.CastId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
