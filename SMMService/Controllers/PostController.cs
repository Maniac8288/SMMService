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
        private IProjectService _projectService = new ProjectService();
        // GET: Post
        public ActionResult Index(int id)
        {
            var post = _postService.GetPostById(id);
            return View(post);
        }
        public ActionResult Add(int id)
        {
            var post = _projectService.GetProject(id);
            return View(post.Value);
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
            return Json(new { IsSuccess = response.IsSuccess, Message = response.Message, Url = Url.Action("Index","Post",new { id = response.Value}) });
        }
    }
}