﻿//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using RealNest.Web.Models.Foundations.Houses;

namespace RealNest.Web.Models.Foundations.Pictures
{
    public class Picture
    {
        public int PictureId { get; set; }
        public string ImageUrl { get; set; }
        public int HouseId { get; set; }
        public House House { get; set; }
    }
}