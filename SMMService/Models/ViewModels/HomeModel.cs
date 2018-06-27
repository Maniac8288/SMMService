using SMM.IServices.Models.Project;
using SMM.IServices.Models.User.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMM.Web.Models.ViewModels
{
    public class HomeModel
    {
        /// <summary>
        /// Проекты пользователя
        /// </summary>
        public List<ProjectModel> Projects { get; set; }
        /// <summary>
        /// Группы пользователя
        /// </summary>
        public List<GroupModel> Groups { get; set; }
    }
}