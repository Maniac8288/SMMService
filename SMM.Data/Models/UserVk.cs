using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Data.Models
{
    /// <summary>
    /// Пользователь ВК
    /// </summary>
    public class UserVk
    {
        /// <summary>
        /// Ид пользователя
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Ид пользовтаеля ВК
        /// </summary>
        public int? VkId { get; set; }
        /// <summary>
        /// Токен доступа от вк
        /// </summary>
        public string AccessToken { get; set; }

        #region Связанные объекты 

        /// <summary>
        /// Связь с таблицей Юзер
        /// </summary>
        public User User { get; set; }
        #endregion
    }
}
