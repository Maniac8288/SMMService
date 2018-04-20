using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Data.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Ид пользователя
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Ид социальной сети
        /// </summary>
        public int? VkId { get; set; }
        /// <summary>
        /// Ид одноклассников
        /// </summary>
        public string OkId { get; set; }
        /// <summary>
        /// Сссылка на изображение
        /// </summary>
        public string ImageUrl { get; set; } 
        /// <summary>
        /// Токен доступа от вк
        /// </summary>
        public string AccessTokenVk { get; set; }
    }
}
