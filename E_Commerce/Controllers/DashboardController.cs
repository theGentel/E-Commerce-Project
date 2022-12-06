using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [Authorize(Roles = "SuperAdmin,Admin")]
        public ActionResult AddDashboard()
        {
            return View();
        }
    }
}