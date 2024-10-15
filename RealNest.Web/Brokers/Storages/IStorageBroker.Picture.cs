//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using RealNest.Web.Models.Foundations.Pictures;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Picture> InsertPictureAsync(Picture picture);
        ValueTask<IQueryable<Picture>> SelectAllPicturesAsync();
        ValueTask<Picture> SelectPictureByIdAsync(int pictureId);
        ValueTask<Picture> UpdatePictureAsync(Picture picture);
        ValueTask<Picture> DeletePictureAsync(Picture picture);
    }
}
