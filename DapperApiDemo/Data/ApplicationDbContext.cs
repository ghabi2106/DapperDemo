using DapperApiDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApiDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Junction> Junctions { get; set; }
        public DbSet<Cover> Covers { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Paragraph> Paragraphs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
            modelBuilder.Entity<Category>().Ignore(t => t.Books);

            modelBuilder.Entity<Book>()
                .HasOne(c => c.Category).WithMany(e => e.Books).HasForeignKey(c => c.CategoryId);
            modelBuilder.Entity<Book>().Ignore(t => t.Chapters);
            modelBuilder.Entity<Book>().Ignore(t => t.Junctions);
            modelBuilder.Entity<Book>().Ignore(t => t.Cover);
            modelBuilder.Entity<Book>().Ignore(t => t.Chapters);

            modelBuilder.Entity<Cover>()
                .HasOne(c => c.Book).WithOne(e => e.Cover).HasForeignKey<Cover>(b => b.BookId);

            modelBuilder.Entity<Junction>()
                .HasOne(c => c.Book).WithMany(e => e.Junctions).HasForeignKey(c => c.BookId);

            modelBuilder.Entity<Junction>()
                .HasOne(c => c.Author).WithMany(e => e.Junctions).HasForeignKey(c => c.AuthorId);

            modelBuilder.Entity<Chapter>()
                .HasOne(c => c.Book).WithMany(e => e.Chapters).HasForeignKey(c => c.BookId);
            modelBuilder.Entity<Chapter>().Ignore(t => t.Pages);

            modelBuilder.Entity<Page>()
                .HasOne(c => c.Chapter).WithMany(e => e.Pages).HasForeignKey(c => c.ChapterId);
            modelBuilder.Entity<Page>().Ignore(t => t.Paragraphs);
        }
    }
}
