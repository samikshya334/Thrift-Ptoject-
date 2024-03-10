﻿using Microsoft.AspNetCore.Identity;

namespace Thrift_Us.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PicturePath { get; set; }
    }
}
