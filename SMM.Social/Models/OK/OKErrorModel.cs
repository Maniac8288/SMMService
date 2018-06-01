using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Social.Models.OK
{
    public class OKErrorModel
    {
        /// <summary>
        /// Ошибка
        /// </summary>
        public int error_code { get; set; }
        /// <summary>
        /// Сообщение ошибки
        /// </summary>
        public string error_msg { get; set; }
    }
}
