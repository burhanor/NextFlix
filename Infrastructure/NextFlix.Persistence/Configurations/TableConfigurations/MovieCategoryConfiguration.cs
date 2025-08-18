using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class MovieCategoryConfiguration : IEntityTypeConfiguration<MovieCategory>
	{
		public void Configure(EntityTypeBuilder<MovieCategory> builder)
		{
			builder.HasIndex(mc => new { mc.MovieId, mc.CategoryId })
				.IsUnique();
			builder.HasOne(mc => mc.Movie)
				.WithMany(m => m.MovieCategories)
				.HasForeignKey(mc => mc.MovieId)
				.OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(mc => mc.Category)
				.WithMany(c => c.MovieCategories)
				.HasForeignKey(mc => mc.CategoryId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
