using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class UserLoginRegisterController : Controller
    {
        // GET: UserRegister
        [Route("LoginRegister")]
        public ActionResult LoginRegister()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Save(UserRegisterModel obj)
        {
            using (var context = new E_CommerceDBEntities())
            {
                tblUserRegister C = new tblUserRegister()
                {
                    FirstName = obj.FirstName,
                    LastName = obj.LastName,
                    Email = obj.Email,
                    Password = obj.Password,
                    ConformPassword = obj.ConformPassword,
                };
                context.tblUserRegister.Add(C);
                context.SaveChanges();
            }
            return Json(1);
        }
    }
}