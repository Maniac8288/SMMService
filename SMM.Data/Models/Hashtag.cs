using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Data.Models
{
    public class Hashtag
    {
        /// <summary>
        /// Ид
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название хэштэга
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Ид проекта
        /// </summary>
        public int ProjectId { get; set; }

        #region Связанные объекты
        /// <summary>
        /// Связь с таблицой проекта
        /// </summary>
        public Project Project { get; set; }
        /// <summary>
        /// Связь с таблицей постов
        /// </summary>
        public List<Post> Posts { get; set; }
        #endregion
    }


}
