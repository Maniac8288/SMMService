using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Social.Models.Vk.Group
{
    public class ModelGroup
    {
        /// <summary>
        /// Ид группы
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название группы
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///  Фото группы
        /// </summary>
        public string photo_50 { get; set; }
    }
}
