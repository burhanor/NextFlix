using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class MovieLikeConfiguration : IEntityTypeConfiguration<MovieLike>
	{
		public void Configure(EntityTypeBuilder<MovieLike> builder)
		{
		}
	}
}
