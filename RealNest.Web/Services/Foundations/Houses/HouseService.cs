//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using RealNest.Web.Brokers.Storages;
using RealNest.Web.Models.Foundations.Houses;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Services.Foundations.Houses
{
    public class HouseService : IHouseService
    {
        private readonly IStorageBroker storageBroker;

        public HouseService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<House> AddHouseAsync(House house) =>
            await this.storageBroker.InsertHouseAsync(house);

        public async ValueTask<IQueryable<House>> RetrieveAllHousesAsync() =>
           await this.storageBroker.SelectAllHousesAsync();

        public async ValueTask<House> RetrieveHouseByIdAsync(int houseId) =>
            await this.storageBroker.SelectHouseByIdAsync(houseId);

        public async ValueTask<House> ModifyHouseAsync(House house) =>
            await this.storageBroker.UpdateHouseAsync(house);

        public async ValueTask<House> UpdateHouseAsync(House house)
        {
            var existingHouse = await this.storageBroker.SelectHouseByIdAsync(house.Id);

            if (existingHouse != null)
            {
                if (await this.storageBroker.UserExistsAsync(house.UserId))
                {
                    existingHouse.UserId = house.UserId;
                    await this.storageBroker.UpdateHouseAsync(existingHouse);
                    return existingHouse;
                }
                else
                {
                    throw new ArgumentException($"UserId {house.UserId} does not exist.");
                }
            }
            throw new ArgumentException("House does not exist.");
        }
        public async ValueTask<House> RemoveHouseAsync(int houseId)
        {
            House maybeHouse = await this.storageBroker.SelectHouseByIdAsync(houseId);
            return await this.storageBroker.DeleteHouseAsync(maybeHouse);
        }
    }
}
