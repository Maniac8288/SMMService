using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Social.Models.OK.Photos
{
    public class ResponseGetUploadUrlModel
    {
        /// <summary>
        /// Время в милисикундах 
        /// </summary>
        public long expires_ms { get; set; }
        /// <summary>
        /// Ид фотографий
        /// </summary>
        public List<string> photo_ids { get; set;}
        /// <summary>
        /// Ссылка для загрузки фотографйи
        /// </summary>
        public string upload_url { get; set; }
    }
}
