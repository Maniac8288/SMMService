using SMM.IServices.Interface;
using SMM.Services;
using SMM.Social.Services;
using SMM.Web.Infrastructura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMM.Web.Controllers
{
    public class PostController : Controller
    {
        private IUserService _userService = new UserService();
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Archive()
        {
            return View();
        }
        public ActionResult Calendar()
        {
            return View();
        }
        public ActionResult Verification()
        {
            return View();
        }

        public ActionResult PublicationOk()
        {
            var client = new OKService();
            var userId = new WebUser().UserId;
            var refresh_token = _userService.GetAccessTokenOk(userId);
            if (refresh_token.IsSuccess)
            {
                var accessToken = client.GetAccessTokenWithRefreshToken(refresh_token.Value);
                if (!accessToken.IsSuccess)
                    return Json(accessToken);
                var postResponse = client.Post(accessToken.Value.access_token,"Тестовая запись", "59033221267498");
             
                return Json(postResponse);
            }
            return Json("");
        }
    }
}