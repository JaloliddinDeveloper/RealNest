//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using RealNest.Web.Models.Foundations.Houses;
using System;
using System.Collections.Generic;

namespace RealNest.Web.Models.Foundations.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<House> Houses { get; set; }
    }
}
