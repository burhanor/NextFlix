using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class LogConfiguration : IEntityTypeConfiguration<Log>
	{
		public void Configure(EntityTypeBuilder<Log> builder)
		{
		}
	}
}
