using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        // GET: Contact
        [Route("Contactus")]
        public ActionResult Contactus()
        {
            return View();
        }
    }
}