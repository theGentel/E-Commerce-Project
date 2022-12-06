using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class MyAccountController : Controller
    {
        // GET: MyAccount
        [Route("Account")]
        public ActionResult Account()
        {
            return View();
        }
    }
}