//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using RealNest.Web.Models.Foundations.Houses;
using System;

namespace RealNest.Web.Models.Foundations.Pictures
{
    public class Picture
    {
        public Guid PictureId { get; set; }
        public string ImageUrl { get; set; }
        public Guid HouseId { get; set; }
        public House House { get; set; }
    }
}
