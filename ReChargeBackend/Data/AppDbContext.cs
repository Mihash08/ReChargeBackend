using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Configuration;
using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ReCharge.Data
{
    public class AppDbContext : DbContext
    {
        //todo add dbSets like below
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public AppDbContext() : base()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // "Host=db;Database=example;Username=postgres;Password=example"
            // "Host=localhost; Port=5432; Database=postgres; User Id=postgres; Password=39011093;"
            optionsBuilder.UseNpgsql("Host=db;Database=example;Username=postgres;Password=example");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //todo build model  

            base.OnModelCreating(modelBuilder);

            // Reservations

            //modelBuilder.Entity<Reservation>().HasOne(r => r.User)
            //    .WithMany(u => u.Reservations)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<User>().HasMany(u => u.Reservations)
            //    .WithOne(r => r.User).HasForeignKey(u => u.UserId);
        }

    }
}
