using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Social.Models.OK.Group
{
    /// <summary>
    /// Модель группы от одноклассников
    /// </summary>
    public class ModelGroupOk
    {
        /// <summary>
        /// Ид группы
        /// </summary>
        public string groupId { get; set; }
        /// <summary>
        /// Ид группы
        /// </summary>
        public string uid { get; set; }
        /// <summary>
        /// Название группы
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Ид фотографии
        /// </summary>
        public string photo_id { get; set; }
        /// <summary>
        /// Ссылка на фотографию
        /// </summary>
        public string PhotoUrl { get; set; }
    }
}
