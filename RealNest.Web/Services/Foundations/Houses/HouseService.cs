//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using RealNest.Web.Brokers.Storages;
using RealNest.Web.Models.Foundations.Houses;

namespace RealNest.Web.Services.Foundations.Houses
{
    public class HouseService : IHouseService
    {
        private readonly IStorageBroker storageBroker;

        public HouseService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<House> AddHouseAsync(House house) =>
            await this.storageBroker.InsertHouseAsync(house);

        public IQueryable<House> RetrieveAllHouses() =>
            this.storageBroker.SelectAllHouses();

        public async ValueTask<House> RetrieveHouseByIdAsync(int houseId) =>
            await this.storageBroker.SelectHouseByIdAsync(houseId);

        public async ValueTask<House> ModifyHouseAsync(House house) =>
            await this.storageBroker.UpdateHouseAsync(house);

        public async ValueTask<House> RemoveHouseAsync(int houseId)
        {
            House maybeHouse = await this.storageBroker.SelectHouseByIdAsync(houseId);
            return await this.storageBroker.DeleteHouseAsync(maybeHouse);
        }
    }
}
