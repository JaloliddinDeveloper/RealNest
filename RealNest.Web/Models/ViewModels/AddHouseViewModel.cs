//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace RealNest.Web.Models.ViewModels
{
    public class AddHouseViewModel
    {
        [Required(ErrorMessage = "Sarlavha majburiy.")]
        [StringLength(100, ErrorMessage = "Sarlavha 100 belgidan oshmasligi kerak.")]
        public string Title { get; set; }
    }
}
