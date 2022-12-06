using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class UserLoginController : Controller
    {
        // GET: UserLogin
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Save(UserLoginModel obj)
        {
            E_CommerceDBEntities objj = new E_CommerceDBEntities();
            using (var context = new E_CommerceDBEntities())
            {
                tblUserLogin C = new tblUserLogin()
                {
                    Email = obj.Email,
                    Password = obj.Password,
                };
                context.tblUserLogin.Add(C);
                context.SaveChanges();
            }
            return Json(1);
        }
    }
}