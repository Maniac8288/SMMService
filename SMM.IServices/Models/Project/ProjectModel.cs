using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SMM.IServices.Models.Project
{
    public class ProjectModel
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
        /// Файл изображения (для сохранения)
        /// </summary>
        public HttpPostedFileBase ImageFile { get; set; }
        /// <summary>
        /// Ссылка на изображение
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// Дата создание проекта
        /// </summary>
        public DateTime DateCreate { get; set; }
    }
}
