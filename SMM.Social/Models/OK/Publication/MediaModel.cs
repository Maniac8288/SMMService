using SMM.Social.Models.OK.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Social.Models.OK.Publication
{
    public class MediaModel
    {
        /// <summary>
        /// Тип поста
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// Текст поста
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// Ид видео
        /// </summary>
        public string movieId { get; set; }
        /// <summary>
        /// list - список фотографий
        /// </summary>
        public List<MediaPhotoModel> list { get; set; }
    }
}
