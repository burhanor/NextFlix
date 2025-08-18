using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class LoginConfiguration : IEntityTypeConfiguration<Login>
	{
		public void Configure(EntityTypeBuilder<Login> builder)
		{
		}
	}
}
