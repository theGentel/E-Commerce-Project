using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace E_Commerce.Controllers
{
    public class CreateUserController : Controller
    {
        // GET: CreateUser
        public ActionResult CreateloginUser()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Save(CreateUserModel obj)
        {
            //E_CommerceDBEntities objj = new E_CommerceDBEntities();

            using (var context = new E_CommerceDBEntities())
            {
                tblAdminRegister AR = new tblAdminRegister()
                {
                    UserName = obj.UserName,
                    UserEmail = obj.UserEmail,
                    Password = obj.Password,
                    RoleID = 1,
                    StatusID =1,
                    Image = UploadImage(obj.ImageFile),
                };
                context.tblAdminRegister.Add(AR);
                context.SaveChanges();
            }

            return Json(1);
        }


        public string UploadImage(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string filename = Path.GetFileName(file.FileName);
                string folderpath = Path.Combine("~/Content/assets/CreateUserImage", filename);
                file.SaveAs(Server.MapPath(folderpath));
                return "/Content/assets/CreateUserImage/" + filename;
            }
            else
            {
                return null;
            }
        }

        public ActionResult Login(LoginModel obj)
        {
            using(var context=new E_CommerceDBEntities())
            {
                var Isvalid = context.tblAdminRegister.Where(x => x.UserEmail == obj.Email && x.Password == obj.Password).FirstOrDefault();
                
                if(Isvalid !=null)
                {
                    FormsAuthentication.SetAuthCookie(Isvalid.UserName, false);
                    return RedirectToAction("AddDashboard", "Dashboard");
                }
                return View();
            }
        }
        public ActionResult Logout(LoginModel obj)
        {
             FormsAuthentication.SignOut();
             return RedirectToAction("Login","CreateUser");

        }

    }
}