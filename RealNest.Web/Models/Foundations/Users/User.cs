//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using Microsoft.AspNetCore.Identity;
using RealNest.Web.Models.Foundations.Houses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealNest.Web.Models.Foundations.Users
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<House> Houses { get; set; }
    }
}
