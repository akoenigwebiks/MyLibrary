﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyLibrary.Data;

#nullable disable

namespace MyLibrary.Migrations
{
    [DbContext(typeof(MyLibraryContext))]
    [Migration("20240801142522_sheld-unique-number")]
    partial class shelduniquenumber
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyLibrary.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<int>("BookSetId")
                        .HasColumnType("int");

                    b.Property<decimal>("Height")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5, 2)");

                    b.Property<int?>("ShelfId")
                        .HasColumnType("int");

                    b.Property<decimal>("Thickness")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookId");

                    b.HasIndex("BookSetId");

                    b.HasIndex("ShelfId");

                    b.ToTable("Books", (string)null);
                });

            modelBuilder.Entity("MyLibrary.Models.BookSet", b =>
                {
                    b.Property<int>("BookSetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookSetId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookSetId");

                    b.ToTable("BookSets", (string)null);
                });

            modelBuilder.Entity("MyLibrary.Models.Library", b =>
                {
                    b.Property<int>("LibraryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LibraryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LibraryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Libraries", (string)null);

                    b.HasData(
                        new
                        {
                            LibraryId = 1,
                            Name = "Main Library"
                        });
                });

            modelBuilder.Entity("MyLibrary.Models.Shelf", b =>
                {
                    b.Property<int>("ShelfId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShelfId"));

                    b.Property<decimal>("Height")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5, 2)");

                    b.Property<int>("LibraryId")
                        .HasColumnType("int");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Width")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("ShelfId");

                    b.HasIndex("LibraryId");

                    b.HasIndex("Number")
                        .IsUnique();

                    b.ToTable("Shelves", (string)null);
                });

            modelBuilder.Entity("MyLibrary.Models.Book", b =>
                {
                    b.HasOne("MyLibrary.Models.BookSet", "BookSet")
                        .WithMany("Books")
                        .HasForeignKey("BookSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyLibrary.Models.Shelf", "Shelf")
                        .WithMany("Books")
                        .HasForeignKey("ShelfId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("BookSet");

                    b.Navigation("Shelf");
                });

            modelBuilder.Entity("MyLibrary.Models.Shelf", b =>
                {
                    b.HasOne("MyLibrary.Models.Library", "Library")
                        .WithMany("Shelves")
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Library");
                });

            modelBuilder.Entity("MyLibrary.Models.BookSet", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("MyLibrary.Models.Library", b =>
                {
                    b.Navigation("Shelves");
                });

            modelBuilder.Entity("MyLibrary.Models.Shelf", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
