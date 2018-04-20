using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Social.Models.Vk
{
    /// <summary>
    /// Модель ответа с получение ключа
    /// </summary>
    public class AccessTokenResponse
    {
        /// <summary>
        /// Ключ доступа
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// Время жизни ключа доступа
        /// </summary>
        public string expires_in { get; set; }
        /// <summary>
        /// Ид пользователя в вк
        /// </summary>
        public int user_id { get; set; }
    }
}
