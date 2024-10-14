//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.AspNetCore.Http;
using RealNest.Web.Models.Foundations.Houses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealNest.Web.Models.ViewModels
{
    public class AddHouseViewModel
    {
        [Required(ErrorMessage = "Sarlavha majburiy.")]
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public double SquareFootage { get; set; }
        public ListingType ListingType { get; set; }
        public string ContactInformation { get; set; }
        public List<IFormFile> HouseImages { get; set; }
    }
}