//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.EntityFrameworkCore;
using RealNest.Web.Models.Foundations.Houses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<House> Houses { get; set; }

        public async ValueTask<House> InsertHouseAsync(House house) =>
           await InsertAsync(house);

        public async ValueTask<IQueryable<House>> SelectAllHousesAsync() =>
           await SelectAllAsync<House>();

        public async ValueTask<House> SelectHouseByIdAsync(int houseId) =>
           await SelectAsync<House>(houseId);

        public async ValueTask<House> UpdateHouseAsync(House house) =>
           await UpdateAsync(house);

        public async ValueTask<House> DeleteHouseAsync(House house) =>
          await DeleteAsync(house);
        public async ValueTask<IQueryable<House>> SelectHousesByUserIdAsync(Guid userId)
        {
            IQueryable<House> userHouses = await SelectAllAsync<House>();
            return userHouses.Where(h => h.UserId == userId);
        }

        public async Task<House> SelectHouseWithPictures(int houseId)
        {
            return await this.Houses
                .Include(h => h.Pictures)
                .FirstOrDefaultAsync(h => h.Id == houseId);
        }
        public async Task<List<House>> SelectHousesWithPicturesAsync()
        {
            return await this.Houses
                .Include(h => h.Pictures)
                .ToListAsync();
        }

        public async Task<List<House>> SelectHouseForBuyWithPicturesAsync()
        {
            return await this.Houses
                .Include(h => h.Pictures)
                .Where(h => h.ListingType == ListingType.Sotish) 
                .ToListAsync();
        }

        public async Task<List<House>> SelectHouseForRentWithPicturesAsync()
        {
            return await this.Houses
                .Include(h => h.Pictures)
                .Where(h => h.ListingType == ListingType.IjaragaBerish)
                .ToListAsync();
        }

        public async Task<List<House>> SearchHousesAsync(string searchInput)
        {
            string searchPattern = "%" + searchInput + "%";

            return await this.Houses
                .Include(h => h.Pictures)
                .Where(h => EF.Functions.Like(h.Title, searchPattern) ||
                            EF.Functions.Like(h.Description, searchPattern) ||
                            EF.Functions.Like(h.Address, searchPattern) ||
                            EF.Functions.Like(h.Location, searchPattern))
                .ToListAsync();
        }

        public async Task<List<House>> SelectNewHousesWithPicturesAsync()
        {
            return await this.Houses
                .Include(h => h.Pictures)             
                .Where(h => h.ExpirationDate > DateTime.Now) 
                .ToListAsync();
        }




        public async Task<List<House>> GetExpiredHousesIjaragaBerishWithPicturesAsync()
        {
            return await this.Houses
                .Include(h => h.Pictures) // Include related pictures
                .Where(h => h.ExpirationDate <= DateTime.Now && h.ListingType == ListingType.IjaragaBerish) // Filter by expiration and ListingType IjaragaBerish
                .ToListAsync(); // Convert the result to a list
        }


        public async Task<List<House>> GetExpiredHousesSotishWithPicturesAsync()
        {
            return await this.Houses
                .Include(h => h.Pictures) // Include related pictures
                .Where(h => h.ExpirationDate <= DateTime.Now && h.ListingType == ListingType.Sotish) // Filter by expiration and ListingType IjaragaBerish
                .ToListAsync(); // Convert the result to a list
        }

    }
}
