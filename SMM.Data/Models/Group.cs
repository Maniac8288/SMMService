using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Data.Models
{
    public class Group
    {
        /// <summary>
        /// Ид 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название группы
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ид пользователя к которому пренадлижит группа
        /// </summary>
        public int UserId { get; set; }
        #region Связанные объекты 
        /// <summary>
        /// Пользователь к которому пренадлижит группа
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Список проектов которые есть у группы
        /// </summary>
        public List<Project> Projects { get; set; }
        #endregion
    }
}
