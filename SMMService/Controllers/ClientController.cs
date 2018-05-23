using SMM.IServices.Interface;
using SMM.IServices.Models.Group;
using SMM.Services;
using SMM.Social.Services;
using SMM.Web.Infrastructura;
using SMM.Web.Models;
using SMM.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMM.Web.Controllers
{
    public class ClientController : Controller
    {
        private IUserService _userService = new UserService();
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Settings(int id)
        {
            var social = new List<ModelSocial>
            {
                new ModelSocial
                {
                    Groups = GetGroupsVK(),
                    Name = "vk"
                },
                new ModelSocial {
                    Groups = GetGroupsOK(),
                    Name = "ok"
                }
            };

            var model = new ModelSettingClient
            {
                Social = social
            };

            return View(model);
        }
        public ActionResult SettingsPerson()
        {
            return View();
        }
        public ActionResult Statistic()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Team()
        {
            return View();
        }

        public List<ModelGroup> GetGroupsVK()
        {
            var client = new VKService();
            var userId = new WebUser().UserId;
            var accessToken = _userService.GetAccessTokenVk(userId);
            if (accessToken.IsSuccess)
            {
                var groupsVk = client.GetGroup(accessToken.Value);
                if (!groupsVk.IsSuccess)
                    return new List<ModelGroup>();
                var groups = groupsVk.Value.Select(x => new ModelGroup()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Url = x.photo_50
                }).ToList();
                return groups;
            }
            return new List<ModelGroup>();
        }
        public List<ModelGroup> GetGroupsOK()
        {
            var client = new OKService();
            var userId = new WebUser().UserId;
            var refresh_token = _userService.GetAccessTokenOk(userId);
            if (refresh_token.IsSuccess)
            {
                var accessToken = client.GetAccessTokenWithRefreshToken(refresh_token.Value);
                if (!accessToken.IsSuccess)
                    return new List<ModelGroup>();
                var groupsidsResponse = client.GetGroups(accessToken.Value.access_token);
                if (!groupsidsResponse.IsSuccess)
                    return new List<ModelGroup>();
                var groupsOk = client.GetGroupInfo(groupsidsResponse.Value.groups, accessToken.Value.access_token);
                if (!groupsOk.IsSuccess)
                    return new List<ModelGroup>();
                var groups = groupsOk.Value.Select(x => new ModelGroup()
                {
                    Id = x.uid,
                    Name = x.name,
                    Url = x.PhotoUrl
                }).ToList();
                return groups;
            }
            return new List<ModelGroup>();

        }
    }
}