//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using RealNest.Web.Models.Foundations.Houses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<House> InsertHouseAsync(House house);
        ValueTask<IQueryable<House>> SelectAllHousesAsync();
        ValueTask<House> SelectHouseByIdAsync(int houseId);
        ValueTask<House> UpdateHouseAsync(House house);
        ValueTask<House> DeleteHouseAsync(House house);
        ValueTask<IQueryable<House>> SelectHousesByUserIdAsync(Guid userId);
        Task<House> SelectHouseWithPictures(int houseId);
        Task<List<House>> SelectHousesWithPicturesAsync();

        Task<List<House>> SelectHouseForBuyWithPicturesAsync();
        Task<List<House>> SelectHouseForRentWithPicturesAsync();
        Task<List<House>> SearchHousesAsync(string searchInput);
        Task<List<House>> SelectNewHousesWithPicturesAsync();

        Task<List<House>> GetExpiredHousesIjaragaBerishWithPicturesAsync();
        Task<List<House>> GetExpiredHousesSotishWithPicturesAsync();

    }
}
