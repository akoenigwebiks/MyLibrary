using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Models;

namespace MyLibrary.Data
{
    public class MyLibraryContext : DbContext
    {
        public MyLibraryContext (DbContextOptions<MyLibraryContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure GenreRoom
            modelBuilder.Entity<GenreRoom>()
                .HasMany(g => g.Shelves)
                .WithOne(s => s.GenreRoom)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of GenreRoom if Shelves exist

            // Configure Shelf
            modelBuilder.Entity<Shelf>()
                .HasMany(s => s.Books)
                .WithOne(b => b.Shelf)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of Shelf if Books exist

            modelBuilder.Entity<Shelf>()
                .HasOne(s => s.GenreRoom)
                .WithMany(g => g.Shelves)
                .HasForeignKey(s => s.GenreRoomId)
                .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent Shelf deletion affecting GenreRoom, though this is likely not necessary unless GenreRoom deletion is intended to cascade.

            // Configure Book
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Shelf)
                .WithMany(s => s.Books)
                .HasForeignKey(b => b.ShelfId)
                .OnDelete(DeleteBehavior.Cascade); // Allow deletion of Book without restriction

            // Configure BookSet
            modelBuilder.Entity<BookSet>()
                .HasOne(bs => bs.GenreRoom)
                .WithMany()
                .HasForeignKey(bs => bs.GenreRoomId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of GenreRoom from deleting BookSet

            modelBuilder.Entity<BookSet>()
                .HasMany(bs => bs.Books)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade); // Allow deletion of BookSet to cascade to Books

            modelBuilder.Entity<BookSet>()
                .HasOne(bs => bs.Shelf)
                .WithMany()
                .HasForeignKey(bs => bs.ShelfId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of Shelf from deleting BookSet
        }

        public DbSet<MyLibrary.Models.GenreRoom> GenreRoom { get; set; } = default!;
        public DbSet<MyLibrary.Models.Book> Book { get; set; } = default!;
        public DbSet<MyLibrary.Models.BookSet> BookSet { get; set; } = default!;
        public DbSet<MyLibrary.Models.Shelf> Shelf { get; set; } = default!;

    }
}
