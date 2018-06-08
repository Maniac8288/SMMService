using SMM.IServices.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.IServices.Models.Post
{
    public class PostCalendarModel
    {
        /// <summary>
        /// Ид поста
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название поста
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Дата публикцации
        /// </summary>
        public DateTime DatePublic { get; set; }
        /// <summary>
        /// Социальная сеть где опубликован
        /// </summary>
        public string Soical { get; set; }
    }
}
