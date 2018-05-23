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

        #region Связанные объекты
        /// <summary>
        /// Связь с таблицей User
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Список постов
        /// </summary>
        public List<Post> Posts { get; set; }
        #endregion
    }
}
