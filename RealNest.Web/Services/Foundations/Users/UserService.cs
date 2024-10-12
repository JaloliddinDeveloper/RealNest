//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using RealNest.Web.Brokers.Storages;
using RealNest.Web.Models.Foundations.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Services.Foundations.Users
{
    public class UserService : IUserService
    {
        private readonly IStorageBroker storageBroker;

        public UserService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<User> AddUserAsync(User user) =>
            await this.storageBroker.InsertUserAsync(user);

        public async ValueTask<IQueryable<User>> RetrieveAllUsersAsync() =>
           await this.storageBroker.SelectAllUsersAsync();

        public async ValueTask<User> RetrieveUserByIdAsync(Guid userId) =>
            await this.storageBroker.SelectUserByIdAsync(userId);

        public async ValueTask<User> ModifyUserAsync(User user) =>
            await this.storageBroker.UpdateUserAsync(user);

        public async ValueTask<User> RemoveUserAsync(Guid userId)
        {
            User maybeUser =
                await this.storageBroker.SelectUserByIdAsync(userId);

            return await this.storageBroker.DeleteUserAsync(maybeUser);
        }
    }
}
