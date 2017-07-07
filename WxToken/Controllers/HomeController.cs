using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WxToken.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string echoStr = Request.QueryString["echostr"];
             
                if (!string.IsNullOrEmpty(echoStr))
                {
                return Content(echoStr);
                }
            return View();
        }
    }
}