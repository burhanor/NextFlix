using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class CastConfiguration : IEntityTypeConfiguration<Cast>
	{
		public void Configure(EntityTypeBuilder<Cast> builder)
		{
			builder.Property(m=>m.Name)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(m => m.Slug)
				.IsRequired()
				.HasMaxLength(50);
			builder.HasIndex(t => t.Slug)
				.IsUnique();


		}
	}
}
