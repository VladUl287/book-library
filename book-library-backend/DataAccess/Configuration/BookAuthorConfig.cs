using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration;

public class BookAuthorConfig : IEntityTypeConfiguration<BookAuthor>
{
    public void Configure(EntityTypeBuilder<BookAuthor> builder)
    {
        builder.HasKey(e => new { e.BookId, e.AuthorId });

        builder.HasOne(e => e.Book)
            .WithMany(b => b.BooksAuthors)
            .HasForeignKey(eb => eb.BookId);

        builder.HasOne(e => e.Author)
            .WithMany(c => c.BooksAuthors)
            .HasForeignKey(ec => ec.AuthorId);
    }
}