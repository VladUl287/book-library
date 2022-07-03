using DataAccess.Configuration;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> contextOptions): base(contextOptions)
        {
            Database.EnsureCreated();
        }

        public DbSet<Book> Books { get; init; }
        public DbSet<Author> Authors { get; init; }
        public DbSet<Review> Reviews { get; init; }
        public DbSet<User> Users { get; init; }
        public DbSet<Role> Roles { get; init; }
        public DbSet<UserToken> UsersTokens { get; init; }
        public DbSet<BookAuthor> BooksAuthors { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
           
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BookConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new AuthorConfig());
            modelBuilder.ApplyConfiguration(new ReviewConfig());
            modelBuilder.ApplyConfiguration(new BookmarkConfig());
            modelBuilder.ApplyConfiguration(new UserTokenConfig());
            modelBuilder.ApplyConfiguration(new CollectionConfig());
        }
    }
}
