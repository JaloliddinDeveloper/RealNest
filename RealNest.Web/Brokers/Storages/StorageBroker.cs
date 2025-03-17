//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RealNest.Web.Models.Foundations.Admins;
using RealNest.Web.Models.Foundations.Houses;
using RealNest.Web.Models.Foundations.Users;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Brokers.Storages
{
    public partial class StorageBroker : DbContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            Database.Migrate(); 
        }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            string connection =
                this.configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseMySql(connection,
                ServerVersion.AutoDetect(connection));
        }

        private async ValueTask<T> InsertAsync<T>(T @object)
        {
            this.Entry(@object).State = EntityState.Added;
            await this.SaveChangesAsync();
            DetachEntity(@object);

            return @object;
        }
        private async ValueTask<IQueryable<T>> SelectAllAsync<T>() where T : class =>
            this.Set<T>();

        private async ValueTask<T> SelectAsync<T>(params object[] @objectIds) where T : class =>
            await this.FindAsync<T>(@objectIds);

        private async ValueTask<T> UpdateAsync<T>(T @object) where T : class
        {
            using var broker = new StorageBroker(this.configuration);
            broker.Entry(@object).State = EntityState.Modified;
            await broker.SaveChangesAsync();

            return @object;
        }

        private async ValueTask<T> DeleteAsync<T>(T @object)
        {
            this.Entry(@object).State = EntityState.Deleted;
            await this.SaveChangesAsync();
            DetachEntity(@object);

            return @object;
        }

        private void DetachEntity<T>(T @object)
        {
            this.Entry(@object).State = EntityState.Detached;
        }
        public override void Dispose() { }

        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id); 

            modelBuilder.Entity<User>()
                .HasMany(u => u.Houses)       
                .WithOne(h => h.User)         
                .HasForeignKey(h => h.UserId) 
                .OnDelete(DeleteBehavior.Cascade); 
           
            modelBuilder.Entity<House>()
                .HasKey(h => h.Id); 

            modelBuilder.Entity<House>()
                .HasMany(h => h.Pictures)      
                .WithOne(p => p.House)         
                .HasForeignKey(p => p.HouseId) 
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
