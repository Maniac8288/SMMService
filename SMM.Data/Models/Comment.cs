using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Data.Models
{
    public class Comment
    {
        /// <summary>
        /// Ид
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Содержание комментария
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Ид пользователя
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Ид поста
        /// </summary>
        public int PostId { get; set; }
        /// <summary>
        /// Дата публикации комментария
        /// </summary>
        public DateTime DateCreate { get; set; }
        /// <summary>
        /// Статус комментария (EnumStatusComment)
        /// </summary>
        public int Status { get; set; }

        #region Связь с таблицами
        /// <summary>
        /// Пользователь который оставил комментарий
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Пост у которого был оставлен комментарий
        /// </summary>
        public Post Post { get; set; }
        #endregion
    }
}
