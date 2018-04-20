using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Social.Models.OK
{
   public class OKGetInfoResponse
    {
        public int objectId { get; set; }
        public string objectIdStr { get; set; }
        public string type { get; set; }
        public int error_code { get; set; }
        public string error_msg { get; set; }
    }
}
