//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using RealNest.Web.Models.Foundations.Houses;
using System;
using System.Text.Json.Serialization;

namespace RealNest.Web.Models.Foundations.Pictures
{
    public class Picture
    {
        public Guid Id { get; set; } 
        public string ImageUrl { get; set; } 
        public Guid HouseId { get; set; }
        public virtual House House { get; set; } 
    }
}
