//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using RealNest.Web.Models.Foundations.Houses;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Services.Foundations.Houses
{
    public interface IHouseService
    {
        ValueTask<House> AddHouseAsync(House house);
       ValueTask<IQueryable<House>> RetrieveAllHousesAsync();
        ValueTask<House> RetrieveHouseByIdAsync(Guid houseId);
        ValueTask<House> ModifyHouseAsync(House house);
        ValueTask<House> RemoveHouseAsync(Guid houseId);
    }
}
