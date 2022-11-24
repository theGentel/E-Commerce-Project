using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class UserLoginRegister
    {
        public int UserRegisterID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public sbyte Email { get; set; }
        public string Password { get; set; }
        public string ConformPassword { get; set; }
    }
}