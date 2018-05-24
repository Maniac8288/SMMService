using SMM.IServices.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMM.Web.Models.ViewModels
{
    public class SettingProjectModel
    {
        public ProjectModel Project { get; set; }
        public List<ModelSocial> Social { get; set; }
    }
}