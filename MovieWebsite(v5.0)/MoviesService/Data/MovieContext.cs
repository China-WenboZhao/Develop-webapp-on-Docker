using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesService.Models;
using System;

namespace MoviesService.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
        }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<PublishCompany> PublishCompany { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(configueMovie);
            modelBuilder.Entity<PublishCompany>(configueCompany);
            //modelBuilder.Entity<Movie>().ToTable("Movies");
            //modelBuilder.Entity<PublishCompany>().ToTable("PublishCompanys");
            //modelBuilder.Entity<PublishCompany>().HasKey(c=>c.companyID);

        }

        private void configueMovie(EntityTypeBuilder<Movie> ebuilder)
        {
            try
            {
                ebuilder.ToTable("Movie");
                ebuilder.Property(m => m.ID);
                ebuilder.HasKey(m => m.ID);
                ebuilder.Property(m => m.Title);
                ebuilder.Property(m => m.ReleaseDate);
                ebuilder.Property(m => m.Genre);
                ebuilder.Property(m => m.Price);
                //ebuilder.Property(m => m.CompanyID);
                ebuilder.HasOne(m => m.PublishCompany).WithMany().HasForeignKey(m => m.CompanyID);
            }
            catch (Exception e)
            {
                throw e;
            }
           
        }
        private void configueCompany(EntityTypeBuilder<PublishCompany> ebuilder)
        {
            try
            {
                ebuilder.ToTable("PublishCompany");
                ebuilder.HasKey(c => c.CompanyID);
                ebuilder.Property(c => c.CompanyID);
                ebuilder.Property(c => c.CompanyName);
            }
            catch (Exception e)
            {
                throw e;
            }
            

        }

    }
}