using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.IServices.Models.Project
{
    public class HashtagModel
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
    }
}
