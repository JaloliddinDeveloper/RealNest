//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.EntityFrameworkCore;
using RealNest.Web.Models.Foundations.Houses;

namespace RealNest.Web.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<House> Houses { get; set; }

        public async ValueTask<House> InsertHouseAsync(House house) =>
             await InsertAsync(house);

        public IQueryable<House> SelectAllHouses() =>
            SelectAll<House>();
        
        public async ValueTask<House> SelectHouseByIdAsync(int houseId)=>
            await SelectAsync<House>(houseId);

        public async ValueTask<House> UpdateHouseAsync(House house)=>
            await UpdateAsync(house);

        public async ValueTask<House> DeleteHouseAsync(House house)=>
            await DeleteAsync(house);
    }
}
