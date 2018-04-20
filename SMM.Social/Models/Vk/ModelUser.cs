using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Social.Models.Vk
{
    /// <summary>
    /// Модель ответа от получение информации о пользователе
    /// </summary>
    public class ModelUser
    {
        /// <summary>
        /// Ид пользователя в вк
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string first_name { get; set; }
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string last_name { get; set; }
        /// <summary>
        /// Фото
        /// </summary>
        public string photo_max_orig { get; set; }
        /// <summary>
        /// Ссылка на изображение
        /// </summary>
        public string ImageUrl
        {
            get
            {
                return photo_max_orig;
            }

        }
    }
}
