//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using RealNest.Web.Models.Foundations.Pictures;
using RealNest.Web.Models.Foundations.Users;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RealNest.Web.Models.Foundations.Houses
{
    public class House
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public double SquareFootage { get; set; }
        public ListingType ListingType { get; set; }
        public string ContactInformation { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public IEnumerable<Picture> Pictures { get; set; }
    }
}
