using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;
using NextFlix.Persistence.Seeds;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class TagConfiguration : IEntityTypeConfiguration<Tag>
	{
		public void Configure(EntityTypeBuilder<Tag> builder)
		{
			builder.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(t => t.Slug)
				.IsRequired()
				.HasMaxLength(50);
			builder.HasIndex(t => t.Slug)
				.IsUnique();
			builder.HasData(TagSeed.Tags);
		}
	}
}
