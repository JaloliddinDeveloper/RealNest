//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using RealNest.Web.Brokers.Storages;
using RealNest.Web.Models.Foundations.Pictures;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Services.Foundations.Pictures
{
    public class PictureService : IPictureService
    {
        private readonly IStorageBroker storageBroker;

        public PictureService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<Picture> AddPictureAsync(Picture picture) =>
            await this.storageBroker.InsertPictureAsync(picture);

        public async ValueTask<IQueryable<Picture>> RetrieveAllPicturesAsync() =>
            await this.storageBroker.SelectAllPicturesAsync();

        public async ValueTask<Picture> RetrievePictureByIdAsync(int pictureId) =>
            await this.storageBroker.SelectPictureByIdAsync(pictureId);

        public async ValueTask<Picture> ModifyPictureAsync(Picture picture) =>
            await this.storageBroker.UpdatePictureAsync(picture);

        public async ValueTask<Picture> RemovePictureAsync(int pictureId)
        {
            Picture maybePicture =
                await this.storageBroker.SelectPictureByIdAsync(pictureId);
            return await this.storageBroker.DeletePictureAsync(maybePicture);
        }
    }
}
