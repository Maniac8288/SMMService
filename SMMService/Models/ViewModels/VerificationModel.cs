using SMM.IServices.Models.Post;
using SMM.IServices.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMM.Web.Models.ViewModels
{
    public class VerificationModel
    {
        /// <summary>
        /// Пост
        /// </summary>
        public PostModel Post { get; set; }
        /// <summary>
        /// Проект
        /// </summary>
        public ProjectModel Project { get; set; }
    }
}