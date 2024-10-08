//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using RealNest.Web.Models.Foundations.Houses;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<House> InsertHouseAsync(House house);
        IQueryable<House> SelectAllHouses();
        ValueTask<House> SelectHouseByIdAsync(int houseId);
        ValueTask<House> UpdateHouseAsync(House house);
        ValueTask<House> DeleteHouseAsync(House house);
    }
}
