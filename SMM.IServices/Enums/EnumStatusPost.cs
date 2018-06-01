using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.IServices.Enums
{
    public enum EnumStatusPost
    {
        None = 0,
        /// <summary>
        /// Опубликованный
        /// </summary>
        Published = 1,
        /// <summary>
        /// На проверке
        /// </summary>
        OnVerification = 2,
        /// <summary>
        /// Потверждено
        /// </summary>
        Confirmed = 3
    }
}
