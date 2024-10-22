////--------------------------------------------------
//// Copyright (c) Coalition Of Good-Hearted Engineers
//// Free To Use To Find Comfort And Peace
////--------------------------------------------------
//using Microsoft.Extensions.Hosting;
//using System.Threading.Tasks;
//using System.Threading;
//using System;
//using RealNest.Web.Brokers.Storages;

//namespace RealNest.Web.Services.ListingExpirationServices
//{
//    public class ListingExpirationService : BackgroundService
//    {
//        private readonly IStorageBroker storageBroker;

//        public ListingExpirationService(IStorageBroker storageBroker) =>
//            this.storageBroker = storageBroker;

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                await Task.Delay(TimeSpan.FromDays(1), stoppingToken); // Runs once a day
//                await CheckAndExpireListings();
//            }
//        }

//        private async Task CheckAndExpireListings()
//        {
//            var expiredListings = await storageBroker.SelectHousesWithPicturesAsync();
//            foreach (var house in expiredListings)
//            {
//                house.IsActive = false;
//                await storageBroker.InsertHouseAsync(house);
//            }
//        }

//    }
//}
