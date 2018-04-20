using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Social.Models.OK
{
    public class OKUserModel
    {
        /// <summary>
        /// Ид пользователя в ОК
        /// </summary>
        public string uid { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string first_name { get; set; }
        /// <summary>
        /// Фамилия пользователя 
        /// </summary>
        public string last_name { get; set; }
        /// <summary>
        /// Почта пользователя
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Изображение 640x480
        /// </summary>
        public string pic640x480 { get; set; }
        /// <summary>
        /// Ссылка на изображение
        /// </summary>
        public string ImageUrl
        {
            get
            {
                return pic640x480;
            }

        }
    }
}
