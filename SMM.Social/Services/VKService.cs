using Newtonsoft.Json;
using SMM.Social.Models.Vk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace SMM.Social.Services
{
    public class VKService
    {
      

        #region Свойства


        /// <summary>
        /// Ид приложение вк
        /// </summary>
        private static string clientId = WebConfigurationManager.AppSettings["VkClientId"];
        /// <summary>
        /// Секретный ключ приложения вк
        /// </summary>
        private static string secretKey = WebConfigurationManager.AppSettings["VkSecretKey"];
        /// <summary>
        /// Редирект ссылка на наш сайт 
        /// </summary>
        private static string redirectUri = WebConfigurationManager.AppSettings["redirectUriVk"];
        /// <summary>
        /// Ссылка для получение доступа ключа
        /// </summary>
        private static string access_tokenUrl = "https://oauth.vk.com/access_token?";
        /// <summary>
        /// Ссылка для диалога авторизации
        /// </summary>
        private static string authorizeUrl = "https://oauth.vk.com/authorize?";
        /// <summary>
        /// Ссылка для получение информации о пользователе
        /// </summary>
        private static string userGetUrl = "https://api.vk.com/method/users.get?";
        /// <summary>
        /// Ссылка для получение информации о пользователе
        /// </summary>
        private static string sendPostUrl = "https://api.vk.com/method/wall.post?";

        /// <summary>
        /// Версия апи
        /// </summary>
        private static string version = "5.74";

        #endregion

        #region Вход
        /// <summary>
        /// Получить ссылку для открытия диалога авторизации ВК
        /// </summary>
        /// <returns></returns>
        public static string GetUrl()
        {
            return ($"{authorizeUrl}client_id={clientId}&display=page&redirect_uri={redirectUri}&scope=friends,wall,offline&response_type=code&v={version}");
        }

        /// <summary>
        /// Получить ключ доступа 
        /// </summary>
        /// <param name="code">Код полученный с диалога авторизации вк</param>
        /// <returns></returns>
        public AccessTokenResponse GetAccessToken(string code)
        {
            //Создает Get запрос для получение ключа
            var url = ($"{access_tokenUrl}client_id={clientId}&client_secret={secretKey}&redirect_uri={redirectUri}&code={code}");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = (HttpWebResponse)request.GetResponse();
            // Конвертим получены ответ
            var stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string content = sr.ReadToEnd();
            return JsonConvert.DeserializeObject<AccessTokenResponse>(content);

        }
        #endregion

        #region Информация о пользователе
        /// <summary>
        /// Получить информацию о пользователе
        /// </summary>
        /// <param name="userId">Ид вк пользователя</param>
        /// <param name="access_token">Ключ доступа к апи</param>
        /// <returns>Модель ответа о пользователе</returns>
        public ModelUser GetUser(int userId, string access_token)
        {
            //Создает Get запрос для получение ключа
            var url = ($"{userGetUrl}user_ids={userId}&fields=photo_max_orig&access_token={access_token}&v={version}");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = (HttpWebResponse)request.GetResponse();
            // Конвертим получены ответ
            var stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string content = sr.ReadToEnd();
            return JsonConvert.DeserializeObject<UserResponse>(content).response.First();
        }
        #endregion

        #region Публикация

        public string SendPost(int ownerId, string message,string access_token)
        {
            var url = ($"{sendPostUrl}owner_id={ownerId}&message={message}&access_token={access_token}&v={version}");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = (HttpWebResponse)request.GetResponse();
            // Конвертим получены ответ
            var stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string content = sr.ReadToEnd();
            return content;

        }

        #endregion
    }
}
