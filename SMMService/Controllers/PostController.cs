using SMM.IServices.Enums;
using SMM.IServices.Interface;
using SMM.IServices.Models.Post;
using SMM.Services;
using SMM.Social.Services;
using SMM.Web.Infrastructura;
using SMM.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
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
        /// <summary>
        /// Архив посто
        /// </summary>
        /// <param name="projectId">Ид проекта</param>
        /// <returns></returns>
        public ActionResult Archive(int projectId)
        {
            var project = _projectService.GetProject(projectId);
            var posts = _postService.GetPostsProject(projectId);
            var viewModel = new ArhiveModel()
            {
                Posts = posts,
                Project = project.Value
            };
            return View(viewModel);
        }
        public ActionResult Calendar(int id)
        {

            var userId = new WebUser().UserId;
            var project = _projectService.GetProject(id);
            if (!project.IsSuccess || userId != project.Value.CreatorId)
            {
                //todo: сделать переход на страницу с ошибкой!
                return RedirectToAction("Index", "Home");
            }
            var model = new CalendarModel()
            {
                Posts = _postService.GetPostsForCalendar(id),
                Project = project.Value
            };
            return View(model);
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
        public ActionResult Publication(PostModel model)
        {
            var userId = new WebUser().UserId;
            var response = _postService.Publication(userId, model);
            if (response.IsSuccess)
            {
                //var path = Server.MapPath(WebConfigurationManager.AppSettings["Post"] + response.Value + "/Image/");
                //FileService.UploadPost(path, model.ImageFile);
            }
            return Json(new { IsSuccess = response.IsSuccess, Message = response.Message, Url = Url.Action("Index", "Post", new { id = response.Value }) });
        }

        /// <summary>
        /// Ид поста
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Verification(int postId)
        {
            var userId = new WebUser().UserId;
            var response = _postService.VerificationPost(userId, postId);
            return Json(response);
        }
        #region Комментарии
        /// <summary>
        /// Отпарвить комментарий
        /// </summary>
        /// <param name="postId">Ид поста</param>
        /// <param name="comment">Комментарий</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SendComment(int postId,string comment,EnumStatusComment status)
        {
            var userId = new WebUser().UserId;
            var response = _postService.SendComment(userId, postId, comment,status);
            return Json(response);
        }
        #endregion
    }
}