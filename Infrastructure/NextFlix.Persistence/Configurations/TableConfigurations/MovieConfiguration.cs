using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
	{
		public void Configure(EntityTypeBuilder<Movie> builder)
		{
			builder.Property(m => m.Title)
				.IsRequired()
				.HasMaxLength(200);
			builder.Property(m => m.Slug)
				.IsRequired()
				.HasMaxLength(50);
			builder.HasIndex(t => t.Slug)
				.IsUnique();
			builder.Property(m => m.Description)
				.IsRequired(false)
				.HasMaxLength(1000);
			builder.HasOne(m=>m.User)
				.WithMany(u => u.Movies)
				.HasForeignKey(m => m.UserId)
				.OnDelete(DeleteBehavior.Restrict);

		}
	}
}
