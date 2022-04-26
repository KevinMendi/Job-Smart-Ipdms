using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace IpdmsJob.Models.AppDbContext
{
    public class IpdmsDbContext : DbContext
    {

        public DbSet<IpdmsUser> IpdmsUsers { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<ApplicationType> ApplicationTypes { get; set; }

        public DbSet<OfficeAction> OfficeActions { get; set; }

        public DbSet<EmailSent> EmailSents { get; set; }

        public IpdmsDbContext(DbContextOptions<IpdmsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<IpdmsUser>().ToTable("IpdmsUser");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<Document>().ToTable("Document");
            modelBuilder.Entity<ApplicationType>().ToTable("lk_ApplicationType");
            modelBuilder.Entity<OfficeAction>().ToTable("lk_OfficeAction");
            modelBuilder.Entity<EmailSent>().ToTable("EmailSent");
        }




    }
}
