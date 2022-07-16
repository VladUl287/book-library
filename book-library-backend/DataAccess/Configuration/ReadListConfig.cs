using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration;

public class ReadListConfig : IEntityTypeConfiguration<BookRead>
{
    public void Configure(EntityTypeBuilder<BookRead> builder)
    {
        builder.HasKey(e => new { e.UserId, e.BookId });
    }
}