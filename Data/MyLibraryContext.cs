using Microsoft.EntityFrameworkCore;
using MyLibrary.Models; // Ensure your models are referenced correctly

namespace MyLibrary.Data
{
    public class MyLibraryContext : DbContext
    {
        public MyLibraryContext(DbContextOptions<MyLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Library> Libraries { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookSet> BookSets { get; set; }
        public DbSet<Shelf> Shelves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Custom table names
            modelBuilder.Entity<Library>().ToTable("Libraries");
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<BookSet>().ToTable("BookSets");
            modelBuilder.Entity<Shelf>().ToTable("Shelves");

            // Configure decimal properties with specified precision and scale
            modelBuilder.Entity<Book>()
                .Property(b => b.Height)
                .HasPrecision(5, 2);
            modelBuilder.Entity<Book>()
                .Property(b => b.Thickness)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Shelf>()
                .Property(s => s.Height)
                .HasPrecision(5, 2);
            modelBuilder.Entity<Shelf>()
                .Property(s => s.Width)
                .HasPrecision(5, 2);

            // Configure relationships
            modelBuilder.Entity<Shelf>()
                .HasMany(s => s.Books)
                .WithOne(b => b.Shelf)
                .HasForeignKey(b => b.ShelfId)
                .OnDelete(DeleteBehavior.Cascade); // Cascading delete from Shelf to Books

            // Seed data
            modelBuilder.Entity<Library>().HasData(
                new Library { LibraryId = 1, Name = "Main Library" }
            );

            //// Global filter example (e.g., for soft delete)
            //modelBuilder.Entity<Book>()
            //    .HasQueryFilter(b => EF.Property<bool>(b, "IsDeleted") == false);
        }
    }
}
