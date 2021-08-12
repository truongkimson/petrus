using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using petrus.Models;

namespace petrus.Data
{
    public class petrusDb : DbContext
    {
        protected IConfiguration configuration;

        public petrusDb(DbContextOptions<petrusDb> options) : base(options)
        {
        }
        public DbSet<User>Users { get; set; }
        public DbSet<Admin>Admins { get; set; }
        public DbSet<AdoptionListing>AdoptionListings { get; set; }
        public DbSet<AdoptionRequest> AdoptionRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.UserID).IsUnique();
            modelBuilder.Entity<User>().HasMany(u => u.AdoptionRequests);
            modelBuilder.Entity<User>().HasMany(u => u.AdoptionListings);

            modelBuilder.Entity<Admin>().HasIndex(a => a.AdminId).IsUnique();

            modelBuilder.Entity<AdoptionListing>().HasIndex(al => al.AdoptionListingID);
            //modelBuilder.Entity<AdoptionListing>().HasOne(al => al.User);
            modelBuilder.Entity<AdoptionListing>().HasMany(al => al.AdoptionRequests);

            modelBuilder.Entity<AdoptionRequest>().HasIndex(ar => ar.AdoptionRequestId).IsUnique();
            modelBuilder.Entity<AdoptionRequest>().HasOne(ar => ar.AdoptionListing);
            modelBuilder.Entity<AdoptionRequest>().HasOne(ar => ar.User);



        }
    }
    
}
