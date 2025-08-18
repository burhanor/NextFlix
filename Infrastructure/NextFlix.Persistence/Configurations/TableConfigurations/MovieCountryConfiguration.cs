using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class MovieCountryConfiguration : IEntityTypeConfiguration<MovieCountry>
	{
		public void Configure(EntityTypeBuilder<MovieCountry> builder)
		{
			builder.HasIndex(mc => new { mc.MovieId, mc.CountryId })
				.IsUnique();
			builder.HasOne(mc => mc.Movie)
				.WithMany(m => m.MovieCountries)
				.HasForeignKey(mc => mc.MovieId)
				.OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(mc => mc.Country)
				.WithMany(c => c.MovieCountries)
				.HasForeignKey(mc => mc.CountryId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
