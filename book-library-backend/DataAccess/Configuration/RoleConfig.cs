using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration;

public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasData(new Role[]
        {
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "User"
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "Admin"
            }
        });
    }
}