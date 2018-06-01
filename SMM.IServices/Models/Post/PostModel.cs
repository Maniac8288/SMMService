using SMM.IServices.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.IServices.Models.Post
{
    public class PostModel
    {
        /// <summary>
        /// Ид
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название поста
        /// </summary>
        public string Name { get; set; }
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
        /// Статус пользователя
        /// </summary>
        public EnumStatusPost Status { get; set; }
    }
}
