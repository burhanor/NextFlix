using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class ChannelConfiguration : IEntityTypeConfiguration<Channel>
	{
		public void Configure(EntityTypeBuilder<Channel> builder)
		{
			builder.Property(m => m.Name)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(m => m.Slug)
				.IsRequired()
				.HasMaxLength(50);

			builder.HasIndex(t => t.Slug)
				.IsUnique();
			builder.Property(m => m.Logo)
				.IsRequired(false)
				.HasMaxLength(400);

		}
	}
}
