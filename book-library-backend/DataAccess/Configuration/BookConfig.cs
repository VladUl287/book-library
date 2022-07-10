using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasIndex(e => e.Name)
                .IsUnique();

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(10000);

            builder.Property(e => e.Image);

            builder.Property(e => e.PagesCount)
                .IsRequired();

            builder.Property(e => e.Year)
                .IsRequired();
        }
    }
}
