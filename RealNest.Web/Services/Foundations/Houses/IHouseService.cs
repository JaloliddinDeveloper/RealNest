﻿//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using RealNest.Web.Models.Foundations.Houses;

namespace RealNest.Web.Services.Foundations.Houses
{
    public interface IHouseService
    {
        ValueTask<House> AddHouseAsync(House house);
        IQueryable<House> RetrieveAllHouses();
        ValueTask<House> RetrieveHouseByIdAsync(int houseId);
        ValueTask<House> ModifyHouseAsync(House house);
        ValueTask<House> RemoveHouseAsync(int houseId);
    }
}