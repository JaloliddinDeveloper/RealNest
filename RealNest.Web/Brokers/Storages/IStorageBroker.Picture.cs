﻿//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using RealNest.Web.Models.Foundations.Pictures;

namespace RealNest.Web.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Picture> InsertPictureAsync(Picture picture);
        IQueryable<Picture> SelectAllPictures();
        ValueTask<Picture> SelectPictureByIdAsync(int pictureId);
        ValueTask<Picture> UpdatePictureAsync(Picture picture);
        ValueTask<Picture> DeletePictureAsync(Picture picture);
    }
}