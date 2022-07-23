using DataAccess.Configuration;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> contextOptions) : base(contextOptions)
        {
            Database.EnsureCreated();
        }

        public DbSet<Book> Books { get; init; }
        public DbSet<Genre> Genres { get; init; }
        public DbSet<Author> Authors { get; init; }
        public DbSet<Review> Reviews { get; init; }
        public DbSet<Bookmark> Bookmarks { get; init; }
        public DbSet<Collection> Collections { get; init; }
        public DbSet<BookAuthor> BooksAuthors { get; init; }
        public DbSet<BookGenre> BooksGenres { get; init; }
        public DbSet<BookCollection> BooksCollections { get; init; }
        public DbSet<BookRead> ReadList { get; init; }
        public DbSet<User> Users { get; init; }
        public DbSet<Role> Roles { get; init; }
        public DbSet<UserToken> UsersTokens { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.LogTo((msg) =>
            //{
            //    Console.WriteLine(msg);
            //});
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AuthorConfig());
            modelBuilder.ApplyConfiguration(new BookAuthorConfig());
            modelBuilder.ApplyConfiguration(new BookCollectionConfig());
            modelBuilder.ApplyConfiguration(new BookConfig());
            modelBuilder.ApplyConfiguration(new BookGenreConfig());
            modelBuilder.ApplyConfiguration(new BookmarkConfig());
            modelBuilder.ApplyConfiguration(new CollectionConfig());
            modelBuilder.ApplyConfiguration(new GenreConfig());
            modelBuilder.ApplyConfiguration(new ReviewConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new UserTokenConfig());
            modelBuilder.ApplyConfiguration(new ReadListConfig());

            var books = new List<Book>(100);
            var auhtors = new List<Author>(100);
            var genres = new List<Genre>(10);
            var booksAuthors = new List<BookAuthor>(100);
            var booksGenres = new List<BookGenre>(100);
            for (int i = 0; i < 100; i++)
            {
                books.Add(new Book
                {
                    Id = Guid.NewGuid(),
                    Name = $"Name {i}",
                    Description = $"Description {i}",
                    Image = $"Image {i}",
                    Year = 1990,
                    PagesCount = 200
                });
            }

            for (int i = 0; i < 100; i++)
            {
                auhtors.Add(new Author
                {
                    Id = Guid.NewGuid(),
                    Name = $"Name Author {i}"
                });
            }

            for (int i = 0; i < 10; i++)
            {
                genres.Add(new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = $"Name Genre {i}"
                });
            }

            for (int i = 0; i < 100; i++)
            {
                booksAuthors.Add(new BookAuthor
                {
                    BookId = books[i].Id,
                    AuthorId = auhtors[i].Id
                });
            }

            var j = 0;
            for (int i = 0; i < 100; i++)
            {
                if (j == 10)
                {
                    j = 0;
                }
                booksGenres.Add(new BookGenre
                {
                    BookId = books[i].Id,
                    GenreId = genres[j].Id
                });
                j++;
            }

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasData(books);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasData(auhtors);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasData(genres);
            });

            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.HasData(booksAuthors);
            });

            modelBuilder.Entity<BookGenre>(entity =>
            {
                entity.HasData(booksGenres);
            });
        }
    }
}
