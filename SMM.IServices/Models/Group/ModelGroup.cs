using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.IServices.Models.Group
{
    /// <summary>
    /// Модель группы социальных сетей
    /// </summary>
    public class ModelGroup
    {
        /// <summary>
        /// Ид группы
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Название группы
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ссылка на изображение группы
        /// </summary>
        public string Url { get; set; }
    }
}
