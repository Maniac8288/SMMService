using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Social.Models.OK.Photos
{
    public class MediaPhotoModel
    {
        /// <summary>
        /// * id - параметр token, передаваемый в метод photosV2.commit во время загрузки 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// photoId - id фотографии для которой будет сделана решара
        /// </summary>
        public string photoId { get; set; }
    }
}
