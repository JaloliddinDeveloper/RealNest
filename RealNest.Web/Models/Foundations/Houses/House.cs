//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using RealNest.Web.Models.Foundations.Pictures;
using RealNest.Web.Models.Foundations.Users;
using System;
using System.Collections.Generic;

namespace RealNest.Web.Models.Foundations.Houses
{
    public class House
    {
        public Guid HouseId { get; set; }
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<Picture> Pictures { get; set; }
        public HouseType Type { get; set; } 
    }
}
