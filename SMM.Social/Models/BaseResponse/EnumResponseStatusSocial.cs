using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Social.Models.BaseResponse
{
    /// <summary>
    /// Список статусов ответа
    /// </summary>
    public enum EnumResponseStatusSocial
    {
        /// <summary>
        /// Успешно
        /// </summary>
        Success = 0,
        /// <summary>
        /// Ошибка
        /// </summary>
        Error = 2,
        /// <summary>
        /// Исключение
        /// </summary>
        Exception = 3
    }
}
