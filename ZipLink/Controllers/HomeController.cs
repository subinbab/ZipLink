using ConsumeLayer.URLOperators;
using log4net;
using Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZipLink.Controllers
{
    public class HomeController : Controller
    {
        private readonly IURLConsumes _consumes;
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        public HomeController(IURLConsumes uRLConsumes)
        {
            _consumes = uRLConsumes;
        }
        public ActionResult Index(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var redirectUrl = _consumes.RedirectUrl(url);
                return Redirect(redirectUrl);
            }
            else
            {
                return View();
            }
            
        }
        public JsonResult addURL(string url)
        {
            ViewData["url"] = url;
            var data = new URLClient();
            data.url = url;
            // Fetch client's IP address
            string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            data.clientIp = ipAddress;
            var result = _consumes.UrlUpload(data);

            log.Debug($"User Request from {ipAddress} ip address to shorten url {url}");
            return new JsonResult() { Data=data ,ContentType = "json"};
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}