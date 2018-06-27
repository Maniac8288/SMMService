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
        /// Сссылка на изображение
        /// </summary>
        public string ImageUrl { get; set; }



        #region Связанные объекты 
        /// <summary>
        /// Связь  с таблицей проектов
        /// </summary>
        public List<Project> Projects { get; set; }
        /// <summary>
        /// Связь с таблицей постов
        /// </summary>
        public List<Post> Posts { get; set; }
        /// <summary>
        /// Комментарии
        /// </summary>
        public List<Comment> Comments { get; set; }
        /// <summary>
        /// Список групп которые есть у пользователя 
        /// </summary>
        public List<Group> Groups { get; set; }
        #region Связь с социальными сетями
        /// <summary>
        /// Связь с пользователем ОК
        /// </summary>
        public UserOk UserOk { get; set; }
        /// <summary>
        /// Связь с пользователем ВК
        /// </summary>
        public UserVk UserVk { get; set; }
        #endregion
        #endregion
    }
}
