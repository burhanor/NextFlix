using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class MovieTagConfiguration : IEntityTypeConfiguration<MovieTag>
	{
		public void Configure(EntityTypeBuilder<MovieTag> builder)
		{
			builder.HasIndex(mt => new { mt.MovieId, mt.TagId })
				.IsUnique();
			builder.HasOne(mt => mt.Movie)
				.WithMany(m => m.MovieTags)
				.HasForeignKey(mt => mt.MovieId)
				.OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(mt => mt.Tag)
				.WithMany(t => t.MovieTags)
				.HasForeignKey(mt => mt.TagId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
