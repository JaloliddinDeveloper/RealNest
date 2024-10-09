//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.EntityFrameworkCore;
using RealNest.Web.Models.Foundations.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<User> Users { get; set; }

        public async ValueTask<User> InsertUserAsync(User user) =>
            await InsertAsync(user);

        public async ValueTask<IQueryable<User>> SelectAllUsersAsync() =>
           await SelectAllAsync<User>();

        public async ValueTask<User> SelectUserByIdAsync(int userId)=>
            await SelectAsync<User>(userId);

        public async ValueTask<User> UpdateUserAsync(User user)=>
            await UpdateAsync(user);

        public async ValueTask<User> DeleteUserAsync(User user)=>
            await DeleteAsync(user);
        public async ValueTask<User> SelectUserByEmailAsync(string email)
        {
            using var broker = new StorageBroker(this.configuration);
            return await broker.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async ValueTask<User> SelectUserByIdAsync(Guid userId)
        {
            return await this.Users.FindAsync(userId);
        }
    }
}
