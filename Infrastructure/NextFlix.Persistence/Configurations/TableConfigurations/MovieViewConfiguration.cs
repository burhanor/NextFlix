using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class MovieViewConfiguration : IEntityTypeConfiguration<MovieView>
	{
		public void Configure(EntityTypeBuilder<MovieView> builder)
		{

		}
	}
}
