using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration;

public class BookGenreConfig : IEntityTypeConfiguration<BookGenre>
{
    public void Configure(EntityTypeBuilder<BookGenre> builder)
    {
        builder.HasKey(e => new { e.BookId, e.GenreId });

        builder.HasOne(e => e.Book)
            .WithMany(x => x.BooksGenres)
            .HasForeignKey(ex => ex.BookId);

        builder.HasOne(e => e.Genre)
            .WithMany(x => x.BooksGenres)
            .HasForeignKey(ex => ex.GenreId);
    }
}
