using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace DataAccess.Configuration;

public class ReviewConfig : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Views)
            .IsRequired();

        builder.Property(e => e.Rating)
            .IsRequired();

        builder.Property(e => e.Text)
            .IsRequired()
            .HasMaxLength(1500);
    }
}