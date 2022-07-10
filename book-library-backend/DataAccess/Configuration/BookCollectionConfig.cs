using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Configuration;

public class BookCollectionConfig : IEntityTypeConfiguration<BookCollection>
{
    public void Configure(EntityTypeBuilder<BookCollection> builder)
    {
        builder.HasKey(e => new { e.BookId, e.CollectionId });

        builder.HasOne(e => e.Book)
            .WithMany(x => x.BooksCollections)
            .HasForeignKey(ex => ex.BookId);

        builder.HasOne(e => e.Collection)
            .WithMany(x => x.BooksCollections)
            .HasForeignKey(ex => ex.CollectionId);
    }
}