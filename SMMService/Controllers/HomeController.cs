﻿using SMM.IServices.Enums;
using SMM.IServices.Interface;
using SMM.IServices.Models.User;
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
    public class HomeController : Controller
    {
        private IUserService _userService = new UserService();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SendPostTest()
        {
            var userId = new WebUser().UserId;
            var responseAccessToken = _userService.GetAccessTokenVk(userId);
            if (responseAccessToken.IsSuccess)
            {
                var client = new VKService();
                var response = client.SendPost(165167297, "Hellloooooo", responseAccessToken.Value);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            return Json(responseAccessToken.Message, JsonRequestBehavior.AllowGet);
        }
        #region Авторизация
        /// <summary>
        /// Авторизация через вк
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult AuthorizationVK(string code)
        {
            var client = new VKService();
            var model = client.GetAccessToken(code);
            var user = client.GetUser(model.user_id, model.access_token);
            var userModel = new UserModel()
            {
                FirstName = user.first_name,
                LastName = user.last_name,
                VkId = user.id,
                ImageUrl = user.ImageUrl,

            };
            var userId = 0;
            var isRegister = _userService.CheckUserWithVk(model.user_id);
            if (isRegister.IsSuccess)
                userId = isRegister.Value;
            if (!isRegister.IsSuccess && isRegister.Status != (int)EnumResponseStatus.Exception)
            {
                var responseRegister = _userService.RegisterUserFromSocial(userModel);
                if (responseRegister.IsSuccess)
                    userId = responseRegister.Value;
            }
            if (userId != 0)
            {
                var webUser = new WebUser()
                {
                    IsAuthorized = true,
                    UserId = userId,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    ImageUrl = userModel.ImageUrl
                };
                System.Web.HttpContext.Current.Session["UserSession"] = webUser;

                _userService.SetUserCookie(webUser.UserId);
                _userService.SetAccessTokenVk(model.access_token, userId);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Авторизация через Facebook
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult AuthorizationFacebook(string code,string error)
        {
            return Json(code); 
        }

        /// <summary>
        /// Авторизация через одноклассники
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult AuthorizationOk(string code,string error)
        {
            var client = new OKService();
            var model = client.GetAccessToken(code);
            var user = client.GetCurrentUser(model.access_token);
            var userModel = new UserModel()
            {
                FirstName = user.first_name,
                LastName = user.last_name,
                OkId = user.uid,
                ImageUrl = user.ImageUrl
            };
            var userId = 0;
            var isRegister = _userService.CheckUserWithOk(userModel.OkId);
            if (isRegister.IsSuccess)
                userId = isRegister.Value;
            if (!isRegister.IsSuccess && isRegister.Status != (int)EnumResponseStatus.Exception)
            {
                var responseRegister = _userService.RegisterUserFromSocial(userModel);
                if (responseRegister.IsSuccess)
                    userId = responseRegister.Value;
            }
            if (userId != 0)
            {
                var webUser = new WebUser()
                {
                    IsAuthorized = true,
                    UserId = userId,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    ImageUrl = user.ImageUrl
                    
                };
                System.Web.HttpContext.Current.Session["UserSession"] = webUser;

                _userService.SetUserCookie(webUser.UserId);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}