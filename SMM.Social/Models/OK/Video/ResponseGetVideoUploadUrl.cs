using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Social.Models.OK.Video
{
    public class ResponseGetVideoUploadUrl
    {
        /// <summary>
        /// Ссылка для загрузки видео
        /// </summary>
        public string upload_url {get;set;}
        /// <summary>
        /// Ид видео
        /// </summary>
        public long video_id { get; set; }
    }
}
