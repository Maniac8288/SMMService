using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Data.Models
{
    /// <summary>
    /// Проект
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Ид
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название проекта
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ид создателя проекта 
        /// </summary>
        public int CreatorId { get; set; }
        /// <summary>
        /// Группа вк (если пустая строка то ВК не подключен)
        /// </summary>
        public string GroupVk { get; set; }
        /// <summary>
        /// Группа одноклассников (если пустая строка то ОК не подключен)
        /// </summary>
        public string GroupOK { get; set; }
        /// <summary>
        /// Дата создание проекта
        /// </summary>
        public DateTime DateCreate { get; set; }
        /// <summary>
        /// Статус проекта (EnumStatusProject)
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Основная информация о проекте
        /// </summary>
        public string MainInfo { get; set; }
        /// <summary>
        /// Дополнительная информация о проекте
        /// </summary>
        public string AdditionalInfo { get; set; }
        /// <summary>
        /// Дополнительные карточки в виде JSON строки
        /// </summary>
        public string JsonCards { get; set; }
        /// <summary>
        /// Ид группы 
        /// </summary>
        public int? GroupId { get; set; }

        #region Связанные объекты
        /// <summary>
        /// Связь с таблицей User
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Список постов
        /// </summary>
        public List<Post> Posts { get; set; }
        /// <summary>
        /// Хэштэги которые прендалжит проекту
        /// </summary>
        public List<Hashtag> Hashtags { get; set; }
        /// <summary>
        /// Группа проекта
        /// </summary>
        public Group Group { get; set; }
        #endregion
    }
}
