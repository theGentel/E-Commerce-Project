using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class AboutController : Controller
    {
        // GET: About
        [Route("AboutAdmin")]
        public ActionResult AboutAdmin()
        {
            return View();
        }
        //public ActionResult AboutAdmin()
        //{
        //    return View();
        //}

    }
}