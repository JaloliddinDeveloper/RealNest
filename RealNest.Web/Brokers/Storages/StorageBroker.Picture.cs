//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.EntityFrameworkCore;
using RealNest.Web.Models.Foundations.Pictures;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Picture> Pictures { get; set; }

        public async ValueTask<Picture> InsertPictureAsync(Picture picture)=>
            await InsertAsync(picture);

        public async ValueTask<IQueryable<Picture>> SelectAllPicturesAsync()=>
            await SelectAllAsync<Picture>();
            
        public async ValueTask<Picture> SelectPictureByIdAsync(Guid pictureId)=>
            await SelectAsync<Picture>(pictureId);
       
        public async ValueTask<Picture> UpdatePictureAsync(Picture picture)=>
            await UpdateAsync(picture);
      
        public async ValueTask<Picture> DeletePictureAsync(Picture picture)=>
            await DeleteAsync(picture);
    }
}
