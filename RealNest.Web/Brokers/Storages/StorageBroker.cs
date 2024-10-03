//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.EntityFrameworkCore;
using RealNest.Web.Models.Foundations.Houses;
using RealNest.Web.Models.Foundations.Pictures;
using RealNest.Web.Models.Foundations.Users;

namespace RealNest.Web.Brokers.Storages
{
    public partial class StorageBroker:DbContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = 
                this.configuration.GetConnectionString("DefaultConnectionString");

            optionsBuilder.UseSqlServer(connectionString);
        }
        public override void Dispose() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<House>().ToTable("Houses");
            modelBuilder.Entity<Picture>().ToTable("Pictures");

            modelBuilder.Entity<House>()
                .HasOne(h => h.User)
                .WithMany(u => u.Houses)
                .HasForeignKey(h => h.UserId);

            modelBuilder.Entity<Picture>()
                .HasOne(p => p.House)
                .WithMany(h => h.Pictures)
                .HasForeignKey(p => p.HouseId);
        }
    }
}
