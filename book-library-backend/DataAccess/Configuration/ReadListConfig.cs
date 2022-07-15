using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration;

public class ReadListConfig : IEntityTypeConfiguration<ReadList>
{
    public void Configure(EntityTypeBuilder<ReadList> builder)
    {
        builder.HasKey(e => new { e.UserId, e.BookId });
    }
}