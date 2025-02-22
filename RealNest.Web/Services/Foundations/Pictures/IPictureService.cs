﻿//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using RealNest.Web.Models.Foundations.Pictures;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Services.Foundations.Pictures
{
    public interface IPictureService
    {
        ValueTask<Picture> AddPictureAsync(Picture picture);
        ValueTask<IQueryable<Picture>> RetrieveAllPicturesAsync();
        ValueTask<Picture> RetrievePictureByIdAsync(int pictureId);
        ValueTask<Picture> ModifyPictureAsync(Picture picture);
        ValueTask<Picture> RemovePictureAsync(int houseId);
    }
}
