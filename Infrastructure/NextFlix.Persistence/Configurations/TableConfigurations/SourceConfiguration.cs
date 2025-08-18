using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class SourceConfiguration : IEntityTypeConfiguration<Source>
	{
		public void Configure(EntityTypeBuilder<Source> builder)
		{
			builder.HasIndex(s => s.Title)
				.IsUnique();
			builder.Property(s => s.Title)
				.IsRequired()
				.HasMaxLength(100);
		}
	}
}
