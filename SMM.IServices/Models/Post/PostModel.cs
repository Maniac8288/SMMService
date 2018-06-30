using SMM.IServices.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        #region Файлы 
        /// <summary>
        /// Файл изображения (для сохранения)
        /// </summary>
        public HttpPostedFileBase ImageFile { get; set; }
        /// <summary>
        /// Файл с видео
        /// </summary>
        public HttpPostedFileBase VideoFile { get; set; }
        #endregion
        /// <summary>
        /// Ссылки на фотографию
        /// </summary>
        public List<string> ImagesUrl { get; set; }
       
        /// <summary>
        /// Комментарии 
        /// </summary>
        public List<CommentModel> Comments { get; set; }
        /// <summary>
        /// Ид поста в одноклассниках
        /// </summary>
        public string PostIdOK { get; set; }
    }
}
