using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Social.Models.OK.Group
{
    /// <summary>
    /// Модель ответа от метода getUserGroupsV2
    /// </summary>
    public class OkGetGroupsResponse
    {
        /// <summary>
        /// Группы 
        /// </summary>
        public List<ModelGroupOk> groups { get; set; }
        /// <summary>
        /// Идентификатор постраничного вывода
        /// </summary>
        public string anchor { get; set; }
    }
}
