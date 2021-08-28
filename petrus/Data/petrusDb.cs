using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using petrus.Models;

namespace petrus.Data
{
    public class petrusDb : IdentityDbContext<User, IdentityRole, string>
    {
        protected IConfiguration configuration;

        public petrusDb(DbContextOptions<petrusDb> options) : base(options)
        {
        }
        public DbSet<AdoptionListing>AdoptionListings { get; set; }
        public DbSet<AdoptionRequest> AdoptionRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().HasMany(u => u.AdoptionRequests);
            modelBuilder.Entity<User>().HasMany(u => u.AdoptionListings);

            modelBuilder.Entity<AdoptionListing>().HasIndex(al => al.AdoptionListingID);
            //modelBuilder.Entity<AdoptionListing>().HasOne(al => al.User);
            modelBuilder.Entity<AdoptionListing>().HasMany(al => al.AdoptionRequests);

            modelBuilder.Entity<AdoptionRequest>().HasIndex(ar => ar.AdoptionRequestId).IsUnique();
            modelBuilder.Entity<AdoptionRequest>().HasOne(ar => ar.AdoptionListing);
            modelBuilder.Entity<AdoptionRequest>().HasOne(ar => ar.User);



        }
    }
    
}
