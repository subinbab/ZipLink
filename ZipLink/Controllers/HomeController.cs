using BusinessLayer.URLTrackingOperations;
using ConsumeLayer.URLOperators;
using log4net;
using Microsoft.AspNet.Identity;
using Models.Client;
using Models.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZipLink.Auth;
using ZipLink.Models;

namespace ZipLink.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IURLConsumes _consumes;
        private readonly ConsumeLayer.URLTracking.IURLTrackingOperators _iURLTrackingOperators;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        public HomeController(IURLConsumes uRLConsumes, ConsumeLayer.URLTracking.IURLTrackingOperators iURLTrackingOperators, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _consumes = uRLConsumes;
            _iURLTrackingOperators = iURLTrackingOperators;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public ActionResult Index(string url,string returnUrl)
        {
            if (returnUrl==null)
            {
                if (!string.IsNullOrEmpty(url))
                {
                    var userId = User.Identity.GetUserId();
                    var user = _userManager.FindUserById(userId);
                    // Fetch client's IP address
                    URLClient data = new URLClient();
                    data.userId = userId;
                    data.appUser = user;
                    string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (string.IsNullOrEmpty(ipAddress))
                    {
                        ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                    }
                    data.clientIp = ipAddress;
                    data.url = url;
                    var redirectUrl = _consumes.RedirectUrl(data);
                    URLTrackingClient uRLTrackingClient = new URLTrackingClient();
                    uRLTrackingClient.userId = userId;
                    uRLTrackingClient.appUser = user;
                    uRLTrackingClient.ipAddress = ipAddress;
                    uRLTrackingClient.browserAgent = "Chrome";
                    uRLTrackingClient.shortenUrl = url;
                    uRLTrackingClient.createdBy = userId;
                    _iURLTrackingOperators.UpdateUrlTracker(uRLTrackingClient);
                    return Redirect(redirectUrl);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return Redirect(returnUrl);
            }
            
            
        }
        [HttpPost]
        public async Task<JsonResult> addURL(URLClient urlClient)
        {
            var data = new URLClient();
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var user = _userManager.FindUserById(userId);
                ViewData["url"] = urlClient.url;
                data.userId = userId;
                data.appUser = user;
                data.url = urlClient.url;
                // Fetch client's IP address
                string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                }
                data.clientIp = ipAddress;
                var previewDataObj = new LinkPreviewService();
                var previewData = previewDataObj.GetLinkPreviewAsync((urlClient.url)).Result;
                data.imageUrl = previewData.Image;
                data.text = previewData.Description;
                var result = _consumes.UrlUpload(data);

                log.Debug($"User Request from {ipAddress} ip address to shorten url {urlClient.url}");
                return new JsonResult() { Data = result, ContentType = "json" };
            }
            else
            {
                data.shortenurl = "Invalid link";
                return new JsonResult() { Data =data , ContentType = "json" };
            }
            
        }
        [HttpPost]
        public JsonResult UpdateCTAImpressionCount(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                _iURLTrackingOperators.UpdateCTAImpression(id);
            }
            
            return Json(new { Data = "Impression counted" }, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetCTABannerId()
        {
            var urlData = _consumes.GetRandomBanner();
            if (urlData == null)
            {
                return Json(new { Data = "Invalid" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = new
                {
                    urlId = urlData.id,
                    url = urlData.shortenurl,
                    imageUrl=urlData.imageUrl,
                    desc = urlData.text
                };
                return Json(new { Data = data }, JsonRequestBehavior.AllowGet);
            }
            
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