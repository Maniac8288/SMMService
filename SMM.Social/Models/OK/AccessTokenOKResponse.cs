using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Social.Models.OK
{
    public class AccessTokenOKResponse
    {
        /// <summary>
        /// токен доступа, используемый для формирования запроса к API;
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// время действия токена доступа в секундах.
        /// </summary>
        public string expires_in { get; set; }
        /// <summary>
        ///  токен обновления, который можно использовать в дальнейшем для упрощённой процедуры авторизации. Действителен в течение 30 суток, не возвращается в случае использования токена с удлинённым сроком жизни
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// на данный момент возвращается только session;
        /// </summary>
        public string token_type { get; set; }
        /// <summary>
        ///  код ошибки
        /// </summary>
        public string error { get; set; }
        /// <summary>
        /// описание ошибки.
        /// </summary>
        public string error_description { get; set; }
    }
}
