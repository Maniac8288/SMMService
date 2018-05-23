using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Data.Models
{
    /// <summary>
    /// Список всех социальный сетей
    /// </summary>
    public class Social
    {
        /// <summary>
        /// EnumSocial
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        #region Связанные объекты 
        /// <summary>
        /// Связь с постами
        /// </summary>
        public List<Post> Posts { get; set; }
        #endregion
    }

}
