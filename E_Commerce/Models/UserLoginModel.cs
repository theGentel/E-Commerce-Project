using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class UserLoginModel
    {
        public int UserLoginID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}