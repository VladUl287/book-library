using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration;

public class CollectionConfig : IEntityTypeConfiguration<Collection>
{
    public void Configure(EntityTypeBuilder<Collection> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.DateCreate)
            .IsRequired();

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(e => e.Likes)
            .IsRequired();

        builder.Property(e => e.Image)
           .IsRequired()
           .HasMaxLength(500);

        builder.Property(e => e.Name)
           .IsRequired()
           .HasMaxLength(500);
    }
}