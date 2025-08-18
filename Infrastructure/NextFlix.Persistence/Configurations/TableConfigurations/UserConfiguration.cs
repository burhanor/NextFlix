using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextFlix.Domain.Entities;

namespace NextFlix.Persistence.Configurations.TableConfigurations
{
	internal class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property(u => u.EmailAddress)
				.IsRequired()
				.HasMaxLength(100);
			builder.Property(u => u.Password)
				.IsRequired()
				.HasMaxLength(128);
			builder.Property(u=>u.Nickname)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(u => u.Slug)
				.IsRequired()
				.HasMaxLength(50);
			builder.HasIndex(u => u.EmailAddress)
				.IsUnique();
			builder.HasIndex(u => u.Nickname)
				.IsUnique();
			builder.HasIndex(u => u.Slug)
				.IsUnique();


		}
	}
}
