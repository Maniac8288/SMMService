using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace SMM.Social.Services
{
    public class FacebookService
    {
        #region Свойства


        /// <summary>
        /// Ид приложение вк
        /// </summary>
        private static string clientId = WebConfigurationManager.AppSettings["FacebookClientId"];
        /// <summary>
        /// Секретный ключ приложения вк
        /// </summary>
        private static string secretKey = WebConfigurationManager.AppSettings["FacebookSecretKey"];
        /// <summary>
        /// Редирект ссылка на наш сайт 
        /// </summary>
        private static string redirectUri = WebConfigurationManager.AppSettings["redirectUriFacebook"];
        /// <summary>
        /// Ссылка для получение доступа ключа
        /// </summary>
        private static string access_tokenUrl = "https://oauth.vk.com/access_token?";
        /// <summary>
        /// Ссылка для диалога авторизации
        /// </summary>
        private static string authorizeUrl = "https://www.facebook.com/v2.12/dialog/oauth?";
        /// <summary>
        /// Ссылка для получение информации о пользователе
        /// </summary>
        private static string userGetUrl = "https://api.vk.com/method/users.get?";



        #endregion

        #region Вход
        /// <summary>
        /// Получить ссылку для открытия диалога авторизации ВК
        /// </summary>
        /// <returns></returns>
        public static string GetUrl()
        {
            return ($"{authorizeUrl}client_id={clientId}&redirect_uri={redirectUri}&scope=publish_actions");
        }

        ///// <summary>
        ///// Получить ключ доступа 
        ///// </summary>
        ///// <param name="code">Код полученный с диалога авторизации вк</param>
        ///// <returns></returns>
        //public AccessTokenResponse GetAccessToken(string code)
        //{
        //    //Создает Get запрос для получение ключа
        //    var url = ($"{access_tokenUrl}client_id={clientId}&client_secret={secretKey}&redirect_uri={redirectUri}&code={code}");
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    WebResponse response = (HttpWebResponse)request.GetResponse();
        //    // Конвертим получены ответ
        //    var stream = response.GetResponseStream();
        //    StreamReader sr = new StreamReader(stream);
        //    string content = sr.ReadToEnd();
        //    return JsonConvert.DeserializeObject<AccessTokenResponse>(content);

        //}
        #endregion
    }
}
