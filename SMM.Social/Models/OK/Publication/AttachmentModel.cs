using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Social.Models.OK.Publication
{
    public class AttachmentModel
    {
        /// <summary>
        /// Медия
        /// </summary>
        public List<MediaModel> media { get; set; }
        /// <summary>
        /// Время публикации отложенной темы в формате YYYY-mm-dd hh:mi:ss (в таймзоне Europe/Moscow (GMT+3:00)). Необходимо для создания отложенной темы.
        /// </summary>
        public string publishAt { get; set; }
    }
}
