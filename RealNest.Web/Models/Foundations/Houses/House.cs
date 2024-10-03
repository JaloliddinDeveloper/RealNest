//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using RealNest.Web.Models.Foundations.Pictures;
using RealNest.Web.Models.Foundations.Users;

namespace RealNest.Web.Models.Foundations.Houses
{
    public class House
    {
        public int HouseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Rent { get; set; } 
        public string Location { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public HouseType Type { get; set; } 
    }
}
