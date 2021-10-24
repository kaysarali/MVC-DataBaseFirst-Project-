using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kaysar_Mvc_DataBaseFirst_Project.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error

        public ActionResult FileNotFound()
        {
            return View();
        }
    }
}