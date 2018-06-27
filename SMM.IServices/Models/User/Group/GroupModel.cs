using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.IServices.Models.User.Group
{
   public class GroupModel
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
    }
}
