//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using Microsoft.AspNetCore.Identity;
using RealNest.Web.Models.Foundations.Houses;
using System.ComponentModel.DataAnnotations;

namespace RealNest.Web.Models.Foundations.Users
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }
        public ICollection<House> Houses { get; set; }
    }
}
