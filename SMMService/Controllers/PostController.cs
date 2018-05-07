using SMM.IServices.Interface;
using SMM.IServices.Models.Post;
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
        private IPostService _postService = new PostService();
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

        /// <summary>
        /// Опубликовать пост сейчас
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PublicationNow(PostModel model)
        {
            var userId = new WebUser().UserId;
            var response = _postService.Publication(userId, model);
            return Json(response);
        }
    }
}