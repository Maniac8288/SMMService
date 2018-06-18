using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Data.Models
{
    /// <summary>
    /// Пост
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Ид поста
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Содержание поста
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Дата создание
        /// </summary>
        public DateTime DateCreate { get; set; }
        /// <summary>
        /// Дата публикации в соц сетях
        /// </summary>
        public DateTime DatePublic { get; set; }
        /// <summary>
        /// Ид проекта
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        /// Ид пользователя создавшего пост
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Статус поста (EnumStatusPost)
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Ид поста в одноклассниках
        /// </summary>
        public string PostIdOK { get; set; }

        #region Связаные объекты
        /// <summary>
        /// Связь с проектом
        /// </summary>
        public Project Project { get; set; }
        /// <summary>
        /// Связь с пользователем создавшего пост
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Публикация в каких социальных сетях
        /// </summary>
        public List<Social> Socials { get; set; }
        /// <summary>
        /// Комментарии
        /// </summary>
        public List<Comment> Comments { get; set; }
        #endregion
    }
}
