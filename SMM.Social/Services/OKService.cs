using Newtonsoft.Json;
using SMM.Social.Models.OK;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace SMM.Social.Services
{
    public class OKService
    {
        #region Свойства


        /// <summary>
        /// Ид приложение вк
        /// </summary>
        private static string clientId = WebConfigurationManager.AppSettings["OKClientId"];
        /// <summary>
        /// Публичный ключ приложение
        /// </summary>
        private static string appKey = WebConfigurationManager.AppSettings["OKAppKey"];
        /// <summary>
        /// Секретный ключ приложения вк
        /// </summary>
        private static string secretKey = WebConfigurationManager.AppSettings["OKSecretKey"];
        /// <summary>
        /// Редирект ссылка на наш сайт 
        /// </summary>
        private static string redirectUri = WebConfigurationManager.AppSettings["redirectUriOK"];
        /// <summary>
        /// Ссылка для получение доступа ключа
        /// </summary>
        private static string access_tokenUrl = "https://api.ok.ru/oauth/token.do?";
        /// <summary>
        /// Ссылка для диалога авторизации
        /// </summary>
        private static string authorizeUrl = "https://connect.ok.ru/oauth/authorize?";
        /// <summary>
        /// Доступ к
        /// </summary>
        private static string scope = "VALUABLE_ACCESS,GROUP_CONTENT,GET_EMAIL";
        /// <summary>
        /// Ссылка для использование методов апи
        /// </summary>
        private static string methodsUrl = "https://api.ok.ru/fb.do?";

        #endregion

        #region Вход
        /// <summary>
        /// Получить ссылку для открытия диалога авторизации ВК
        /// </summary>
        /// <returns></returns>
        public static string GetUrl()
        {
            return ($"{authorizeUrl}client_id={clientId}&scope={scope}&response_type=code&redirect_uri={redirectUri}&scope=publish_actions");
        }

        /// <summary>
        /// Получить ключ доступа 
        /// </summary>
        /// <param name="code">Код полученный с диалога авторизации вк</param>
        /// <returns></returns>
        public AccessTokenOKResponse GetAccessToken(string code)
        {

            using (var webClient = new WebClient())
            {
                // Создаём коллекцию параметров
                var pars = new NameValueCollection();

                // Добавляем необходимые параметры в виде пар ключ, значение
                pars.Add("code", code);
                pars.Add("client_id", clientId);
                pars.Add("client_secret", secretKey);
                pars.Add("redirect_uri", redirectUri);
                pars.Add("grant_type", "authorization_code");
                // Посылаем параметры на сервер
                // Может быть ответ в виде массива байт
                var response = webClient.UploadValues(access_tokenUrl, pars);
                string content = webClient.Encoding.GetString(response);
                return JsonConvert.DeserializeObject<AccessTokenOKResponse>(content);
            }
        }
        /// <summary>
        /// Получить id и тип объекта по полной ссылке.
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public OKUserModel GetCurrentUser(string access_token)
        {
            var param = $"application_key={appKey}fields=email,first_name,last_name,pic640x480format=jsonmethod=users.getCurrentUser";
            var sig = getSign(param, access_token);
            using (var webClient = new WebClient())
            {
                // Создаём коллекцию параметров
                var pars = new NameValueCollection();

                // Добавляем необходимые параметры в виде пар ключ, значение
                pars.Add("application_key", appKey);
                pars.Add("fields", "email,first_name,last_name,pic640x480");
                pars.Add("format", "json");
                pars.Add("method", "users.getCurrentUser");
                pars.Add("sig", sig);
                pars.Add("access_token", access_token);
                // Посылаем параметры на сервер
                // Может быть ответ в виде массива байт
                webClient.Encoding = Encoding.UTF8;
                var response = webClient.UploadValues(methodsUrl, pars);                
                string content = webClient.Encoding.GetString(response);
                return JsonConvert.DeserializeObject<OKUserModel>(content);
            }
        }
        private string getSign(string param, string access_token)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return CryptographyService.GetMd5Hash(md5Hash,
                 param + CryptographyService.GetMd5Hash(md5Hash, access_token + secretKey));
            }
        }
        #endregion
    }
}
