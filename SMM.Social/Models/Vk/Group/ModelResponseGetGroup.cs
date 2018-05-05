using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Social.Models.Vk.Group
{
    /// <summary>
    /// Модель ответа от метода GetGroups
    /// </summary>
    public class ModelResponseGetGroup
    {
        /// <summary>
        /// Количество групп
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// Группы
        /// </summary>
        public List<ModelGroup> items { get; set; }
    }
}
