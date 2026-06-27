using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InspireEduCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InspireEduCRM.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<School> Schools => Set<School>();
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Visit> Visits => Set<Visit>();
        public DbSet<VisitBook> VisitBooks => Set<VisitBook>();
        public DbSet<Lead> Leads => Set<Lead>();
        public DbSet<FollowUp> FollowUps => Set<FollowUp>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // VisitBook: composite primary key (the join table needs both IDs together as the key)
            modelBuilder.Entity<VisitBook>()
                .HasKey(vb => new { vb.VisitId, vb.BookId });

            modelBuilder.Entity<VisitBook>()
                .HasOne(vb => vb.Visit)
                .WithMany(v => v.VisitBooks)
                .HasForeignKey(vb => vb.VisitId);

            modelBuilder.Entity<VisitBook>()
                .HasOne(vb => vb.Book)
                .WithMany(b => b.VisitBooks)
                .HasForeignKey(vb => vb.BookId);
            // Lead: one-to-one with School, enforced by making SchoolId unique
            modelBuilder.Entity<Lead>()
                .HasOne(l => l.School)
                .WithOne(s => s.Lead)
                .HasForeignKey<Lead>(l => l.SchoolId);

            modelBuilder.Entity<Lead>()
                .HasIndex(l => l.SchoolId)
                .IsUnique();
            // FollowUp: prevent accidental cascade-delete chains (explained below)
            modelBuilder.Entity<FollowUp>()
                .HasOne(f => f.CustomerServiceRep)
                .WithMany(u => u.FollowUps)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Visit>()
                .HasOne(v => v.SalesRep)
                .WithMany(u => u.Visits)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
