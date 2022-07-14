using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var userRoleId = Guid.Parse("9f948cd2-1b60-4456-8f3f-e972e00ca1e1");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.RoleId)
                .IsRequired()
                .HasDefaultValue(userRoleId);
        }
    }
}
