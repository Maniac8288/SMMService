﻿using SMM.IServices.Interface;
using SMM.Services;
using SMM.Web.Infrastructura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}