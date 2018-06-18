using SMM.IServices.Enums;
using SMM.IServices.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.IServices.Models.Post
{
    public class CommentModel
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
        /// Дата публикации комментария
        /// </summary>
        public DateTime DateCreate { get; set; }
        /// <summary>
        /// Ид пользователя
        /// </summary>
        public UserModel User { get; set; }
        /// <summary>
        /// Ид поста
        /// </summary>
        public int PostId { get; set; }
        /// <summary>
        /// Статус комментария
        /// </summary>
        public EnumStatusComment Status { get; set; }
    }
}
