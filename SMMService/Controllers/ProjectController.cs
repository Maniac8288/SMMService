using SMM.IServices.Interface;
using SMM.IServices.Models.Project;
using SMM.Services;
using SMM.Web.Infrastructura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace SMM.Web.Controllers
{
    public class ProjectController : Controller
    {
        private IProjectService _projectService = new ProjectService();
        private IUserService _userService = new UserService();
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }


        #region Создание/Редактирование проекта
        /// <summary>
        /// Создать проект
        /// </summary>
        [HttpPost]
        public ActionResult CreateProject(string name)
        {
            var userId = new WebUser().UserId;
            var checkAuth = _userService.CheckAuthUser(userId);
            if (!checkAuth.IsSuccess)
                return Json(checkAuth);
            var response = _projectService.CreateProject(name, userId);
            return Json(response);
        }
        /// <summary>
        /// Редактировать проект
        /// </summary>
        [HttpPost]
        public ActionResult EditProject(ProjectModel model)
        {
            var userId = new WebUser().UserId;
            var response = _projectService.EditProject(userId, model);
            if (response.IsSuccess)
            {
                var path = Server.MapPath(WebConfigurationManager.AppSettings["ProjectImage"] + model.Id + "/Image/");
                FileService.UploadImageProject(path, model.ImageFile);
            }
            return Json(response);
        }
        #endregion


        /// <summary>
        /// Получить проекты пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetProjectsUser(string search)
        {
            var userId = new WebUser().UserId;
            var response = _projectService.GetAllProjects(userId,search);
            return Json(response);
        }

        #region Инфо
        /// <summary>
        /// Страница с  редактированием информации
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SettingInfo(int id)
        {
            var userId = new WebUser().UserId;
            var project = _projectService.GetProject(id);
            if (!project.IsSuccess || userId != project.Value.CreatorId)
            {
                //todo: сделать переход на страницу с ошибкой!
                return RedirectToAction("Index", "Home");
            }
            return View(project.Value);
        }
        /// <summary>
        /// Редактировать информацию о проекте
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditInfo(ProjectModel model)
        {
            var response = _projectService.EditInfoProject(model);
            return Json(response);
        }
        #endregion

        #region Группы 
        /// <summary>
        /// Установить/Убрать проект в избранные
        /// </summary>
        /// <param name="projectId">Ид проекта</param>
        /// <param name="isFavorite">Является ли избранным</param>
        /// <returns></returns>
        public ActionResult SetFavorite(int projectId, bool isFavorite)
        {
            var userId = new WebUser().UserId;
            var response = _projectService.SetFavorite(projectId, userId,isFavorite);
            return Json(response);
        }
        /// <summary>
        /// Установить/Убрать проект из архива
        /// </summary>
        /// <param name="projectId">Ид проекта</param>
        /// <param name="isArhive">Архивировать или нет</param>
        /// <returns></returns>
        public ActionResult SetArhive(int projectId, bool isArhive)
        {
            var userId = new WebUser().UserId;
            var response = _projectService.SetArhive(projectId, userId, isArhive);
            return Json(response);
        }
        /// <summary>
        /// Установить группу проекту
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public ActionResult SetGroup(int projectId, int? groupId)
        {
            var response = _projectService.SetGroup(projectId, groupId);
            return Json(response);
        }
        #endregion
    }
}