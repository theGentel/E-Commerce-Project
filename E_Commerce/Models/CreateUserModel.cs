using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class CreateUserModel
    {
        public int AdminID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public int StatusID { get; set; }
        public string Image { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}