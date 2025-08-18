using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;
using NextFlix.Persistence.Seeds;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class CountryConfiguration : IEntityTypeConfiguration<Country>
	{
		public void Configure(EntityTypeBuilder<Country> builder)
		{
			builder.Property(m => m.Name)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(m => m.Slug)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(m => m.Flag)
				.IsRequired(false)
				.HasMaxLength(400);

			builder.HasIndex(t => t.Slug)
				.IsUnique();

			builder.HasData(CountrySeed.Countries);
		}
	}
}
