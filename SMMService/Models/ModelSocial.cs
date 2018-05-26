using SMM.IServices.Models.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMM.Web.Models
{
    public class ModelSocial
    {
        /// <summary>
        /// Название социальной сети
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Группы социальной сети
        /// </summary>
        public List<ModelGroup> Groups { get; set; }
        /// <summary>
        /// Подключена ли социальная сеть у пользователя
        /// </summary>
        public bool IsAuth { get; set; }
        /// <summary>
        /// Выбраная группа
        /// </summary>
        public ModelGroup Group { get; set; }
    }
}