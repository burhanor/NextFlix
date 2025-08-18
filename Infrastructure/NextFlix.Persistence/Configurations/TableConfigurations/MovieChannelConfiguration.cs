using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class MovieChannelConfiguration : IEntityTypeConfiguration<MovieChannel>
	{
		public void Configure(EntityTypeBuilder<MovieChannel> builder)
		{
			builder.HasIndex(mc => new { mc.MovieId, mc.ChannelId })
				.IsUnique();
			builder.HasOne(mc => mc.Movie)
				.WithMany(m => m.MovieChannels)
				.HasForeignKey(mc => mc.MovieId)
				.OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(mc => mc.Channel)
				.WithMany(c => c.MovieChannels)
				.HasForeignKey(mc => mc.ChannelId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
