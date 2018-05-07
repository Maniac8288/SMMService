using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Содержимое поста
        /// </summary>
        public string Content { get; set; }
    }
}
