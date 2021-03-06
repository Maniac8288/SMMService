﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SMM.Social.Models.BaseResponse;
using SMM.Social.Models.OK;
using SMM.Social.Models.OK.Group;
using SMM.Social.Models.OK.Photos;
using SMM.Social.Models.OK.Publication;
using SMM.Social.Models.OK.Video;
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
        private static string scope = "VALUABLE_ACCESS,GROUP_CONTENT,GET_EMAIL,PHOTO_CONTENT,VIDEO_CONTENT";
        /// <summary>
        /// Ссылка для использование методов апи
        /// </summary>
        private static string methodsUrl = "https://api.ok.ru/fb.do?";
        /// <summary>
        /// Ссылка для получение access_token
        /// </summary>
        private static string tokenUrl = "https://api.ok.ru/oauth/token.do?";

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
        public BaseResponseSocial<AccessTokenOKResponse> GetAccessToken(string code)
        {
            try
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
                    var model = JsonConvert.DeserializeObject<AccessTokenOKResponse>(content);
                    if (model.error != null)
                        return new BaseResponseSocial<AccessTokenOKResponse>(EnumResponseStatusSocial.Error, model.error_description);
                    return new BaseResponseSocial<AccessTokenOKResponse>(model);
                }
            }
            catch (Exception e)
            {
                return new BaseResponseSocial<AccessTokenOKResponse>(EnumResponseStatusSocial.Exception, e.Message);
            }
        }
        /// <summary>
        /// Получить id и тип объекта по полной ссылке.
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public BaseResponseSocial<OKUserModel> GetCurrentUser(string access_token)
        {
            try
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
                    var model = JsonConvert.DeserializeObject<OKUserModel>(content);
                    return new BaseResponseSocial<OKUserModel>(model);
                }
            }
            catch (Exception e)
            {
                return new BaseResponseSocial<OKUserModel>(EnumResponseStatusSocial.Exception, e.Message);
            }
        }

        #endregion

        #region Работа с группами

        /// <summary>
        /// Получить список групп(их ид)
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public BaseResponseSocial<OkGetGroupsResponse> GetGroups(string access_token)
        {
            try
            {
                var param = $"application_key={appKey}format=jsonmethod=group.getUserGroupsV2";
                var sig = getSign(param, access_token);
                using (var webClient = new WebClient())
                {
                    // Создаём коллекцию параметров
                    var pars = new NameValueCollection();

                    // Добавляем необходимые параметры в виде пар ключ, значение
                    pars.Add("application_key", appKey);
                    pars.Add("format", "json");
                    pars.Add("method", "group.getUserGroupsV2");
                    pars.Add("sig", sig);
                    pars.Add("access_token", access_token);
                    // Посылаем параметры на сервер
                    // Может быть ответ в виде массива байт
                    webClient.Encoding = Encoding.UTF8;
                    var response = webClient.UploadValues(methodsUrl, pars);
                    string content = webClient.Encoding.GetString(response);
                    var model = JsonConvert.DeserializeObject<OkGetGroupsResponse>(content);
                    return new BaseResponseSocial<OkGetGroupsResponse>(model);
                }
            }
            catch (Exception e)
            {
                return new BaseResponseSocial<OkGetGroupsResponse>(EnumResponseStatusSocial.Exception, e.Message);
            }
        }

        /// <summary>
        /// Получить подробную информацию о группах
        /// </summary>
        /// <param name="groups">Группы</param>
        /// <param name="access_token">Токен</param>
        /// <returns></returns>
        public BaseResponseSocial<List<ModelGroupOk>> GetGroupInfo(List<ModelGroupOk> groups, string access_token)
        {
            try
            {
                var fields = "name,uid,photo_id";
                var uids = string.Join(",", groups.Select(x => x.groupId).ToArray());
                var param = $"application_key={appKey}fields={fields}format=jsonmethod=group.getInfouids={uids}";
                var sig = getSign(param, access_token);
                using (var webClient = new WebClient())
                {
                    // Создаём коллекцию параметров
                    var pars = new NameValueCollection();

                    // Добавляем необходимые параметры в виде пар ключ, значение
                    pars.Add("application_key", appKey);
                    pars.Add("fields", fields);
                    pars.Add("format", "json");
                    pars.Add("method", "group.getInfo");
                    pars.Add("uids", uids);
                    pars.Add("sig", sig);
                    pars.Add("access_token", access_token);
                    // Посылаем параметры на сервер
                    // Может быть ответ в виде массива байт
                    webClient.Encoding = Encoding.UTF8;
                    var response = webClient.UploadValues(methodsUrl, pars);
                    string content = webClient.Encoding.GetString(response);
                    var model = JsonConvert.DeserializeObject<List<ModelGroupOk>>(content);
                    foreach (var item in model)
                    {
                        var photoRes = GetPhoto(access_token, item.photo_id);
                        if (photoRes.IsSuccess)
                            item.PhotoUrl = photoRes.Value.pic50x50;
                    }
                    return new BaseResponseSocial<List<ModelGroupOk>>(model);
                }
            }
            catch (Exception e)
            {
                return new BaseResponseSocial<List<ModelGroupOk>>(EnumResponseStatusSocial.Exception, e.Message);
            }
        }
        #endregion

        #region Публикация

        public BaseResponseSocial<string> Post(string access_token,  string groupId, AttachmentModel attachmentModel)
        {
            try
            {               
                var attachment = JsonConvert.SerializeObject(attachmentModel, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                var param = $"application_key={appKey}attachment={attachment}format=jsongid={groupId}method=mediatopic.posttype=GROUP_THEME";
                var sig = getSign(param, access_token);
                using (var webClient = new WebClient())
                {
                    // Создаём коллекцию параметров
                    var pars = new NameValueCollection();

                    // Добавляем необходимые параметры в виде пар ключ, значение
                    pars.Add("application_key", appKey);
                    pars.Add("attachment", attachment);
                    pars.Add("format", "json");
                    pars.Add("gid", groupId);
                    pars.Add("method", "mediatopic.post");
                    pars.Add("type", "GROUP_THEME");
                    pars.Add("sig", sig);
                    pars.Add("access_token", access_token);
                    // Посылаем параметры на сервер
                    // Может быть ответ в виде массива байт
                    webClient.Encoding = Encoding.UTF8;
                    var response = webClient.UploadValues(methodsUrl, pars);
                    string content = webClient.Encoding.GetString(response);
                    var responsePost = new BaseResponseSocial<string>();
                    if (content.Contains("error_code"))
                    {
                        var responseContent = JsonConvert.DeserializeObject<OKErrorModel>(content);
                        responsePost = new BaseResponseSocial<string>()
                        {
                            Status = responseContent.error_code,
                            Message = responseContent.error_msg
                        };
                    }
                    else
                    {
                        var id = JsonConvert.DeserializeObject<string>(content);
                        responsePost.Status = 0;
                        responsePost.Value = id;
                    }
                    return responsePost;

                }
            }
            catch (Exception e)
            {
                return new BaseResponseSocial<string>(EnumResponseStatusSocial.Exception, e.Message);
            }
        }

        #endregion

        #region Работа с фотографиями
        public BaseResponseSocial<ModelPhoto> GetPhoto(string access_token, string photo_id)
        {
            try
            {
                var fields = "";

                var param = $"application_key={appKey}format=jsonmethod=photos.getPhotoInfophoto_id={photo_id}";
                var sig = getSign(param, access_token);
                using (var webClient = new WebClient())
                {
                    // Создаём коллекцию параметров
                    var pars = new NameValueCollection();

                    // Добавляем необходимые параметры в виде пар ключ, значение
                    pars.Add("application_key", appKey);
                    pars.Add("format", "json");
                    pars.Add("method", "photos.getPhotoInfo");
                    pars.Add("photo_id", photo_id);
                    pars.Add("sig", sig);
                    pars.Add("access_token", access_token);
                    // Посылаем параметры на сервер
                    // Может быть ответ в виде массива байт
                    webClient.Encoding = Encoding.UTF8;
                    var response = webClient.UploadValues(methodsUrl, pars);
                    string content = webClient.Encoding.GetString(response);
                    var model = JsonConvert.DeserializeObject<ResponseGetPhotoInfo>(content);
                    if (model.photo == null)
                    {
                        return new BaseResponseSocial<ModelPhoto>(EnumResponseStatusSocial.Error, "Photo not found");
                    }
                    return new BaseResponseSocial<ModelPhoto>(model.photo);
                }
            }
            catch (Exception e)
            {
                return new BaseResponseSocial<ModelPhoto>(EnumResponseStatusSocial.Exception, e.Message);
            }
        }
        
        /// <summary>
        /// Получить ссылку для загрзки фотографий
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="uid">Идентификатор пользователя, фотографии которого требуется добавить. Укажите uid при вызове этого метода без ключа сессии.</param>
        /// <returns></returns>
        public BaseResponseSocial<ResponseGetUploadUrlModel> GetUploadUrl(string access_token, string gid)
        {
            try
            {
                var param = $"application_key={appKey}format=jsongid={gid}method=photosV2.getUploadUrl";
                var sig = getSign(param, access_token);
                using (var webClient = new WebClient())
                {
                    // Создаём коллекцию параметров
                    var pars = new NameValueCollection();

                    // Добавляем необходимые параметры в виде пар ключ, значение
                    pars.Add("application_key", appKey);
                    pars.Add("format", "json");
                    pars.Add("gid", gid);
                    pars.Add("method", "photosV2.getUploadUrl");
                    pars.Add("sig", sig);
                    pars.Add("access_token", access_token);
                    // Посылаем параметры на сервер
                    // Может быть ответ в виде массива байт
                    webClient.Encoding = Encoding.UTF8;
                    var response = webClient.UploadValues(methodsUrl, pars);
                    string content = webClient.Encoding.GetString(response);
                    var model = JsonConvert.DeserializeObject<ResponseGetUploadUrlModel>(content);

                    return new BaseResponseSocial<ResponseGetUploadUrlModel>(model);
                }
            }
            catch (Exception e)
            {
                return new BaseResponseSocial<ResponseGetUploadUrlModel>(EnumResponseStatusSocial.Exception, e.Message);
            }
        }

        public BaseResponseSocial<string> UploadPhoto(string uploadUrl, string filePath, string photoId)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    var response = client.UploadFile(uploadUrl, filePath);
                    string content = client.Encoding.GetString(response);
                    var obj = JObject.Parse(content);
                    var value = (string)obj.SelectToken($"$.photos.{photoId}.token");

                    return new BaseResponseSocial<string>(value);
                }
            }
            catch (Exception e)
            {
                return new BaseResponseSocial<string>(EnumResponseStatusSocial.Exception, e.Message);
            }
        }
        public BaseResponseSocial UploadVideo(string uploadUrl, string filePath, string photoId)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    var response = client.UploadFile(uploadUrl, filePath);
                    string content = client.Encoding.GetString(response);
                    //var obj = JObject.Parse(content);
                    //var value = (string)obj.SelectToken($"$.photos.{photoId}.token");

                    return new BaseResponseSocial();
                }
            }
            catch (Exception e)
            {
                return new BaseResponseSocial<string>(EnumResponseStatusSocial.Exception, e.Message);
            }
        }
        #endregion

        #region Работа с видео
        /// <summary>
        /// Получить ссылку для загрзки видео
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="uid">Идентификатор пользователя, фотографии которого требуется добавить. Укажите uid при вызове этого метода без ключа сессии.</param>
        /// <returns></returns>
        public BaseResponseSocial<ResponseGetVideoUploadUrl> GetVideoUploadUrl(string access_token,string file_name, long file_size, string gid)
        {
            try
            {
                var param = $"application_key={appKey}file_name={file_name}file_size={file_size.ToString()}format=jsongid={gid}method=video.getUploadUrl";
                var sig = getSign(param, access_token);
                using (var webClient = new WebClient())
                {
                    // Создаём коллекцию параметров
                    var pars = new NameValueCollection();

                    // Добавляем необходимые параметры в виде пар ключ, значение
                    pars.Add("application_key", appKey);
                    pars.Add("file_name", file_name);
                    pars.Add("file_size", file_size.ToString());
                    pars.Add("format", "json");
                    pars.Add("gid", gid);
               
                          
                    pars.Add("method", "video.getUploadUrl");
                    pars.Add("sig", sig);
                    pars.Add("access_token", access_token);
                    // Посылаем параметры на сервер
                    // Может быть ответ в виде массива байт
                    webClient.Encoding = Encoding.UTF8;
                    var response = webClient.UploadValues(methodsUrl, pars);
                    string content = webClient.Encoding.GetString(response);
                    var model = JsonConvert.DeserializeObject<ResponseGetVideoUploadUrl>(content);

                    return new BaseResponseSocial<ResponseGetVideoUploadUrl>(model);
                }
            }
            catch (Exception e)
            {
                return new BaseResponseSocial<ResponseGetVideoUploadUrl>(EnumResponseStatusSocial.Exception, e.Message);
            }
        }
        #endregion

        public BaseResponseSocial<AccessTokenOKResponse> GetAccessTokenWithRefreshToken(string refresh_token)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    // Создаём коллекцию параметров
                    var pars = new NameValueCollection();

                    // Добавляем необходимые параметры в виде пар ключ, значение
                    pars.Add("refresh_token", refresh_token);
                    pars.Add("client_id", clientId);
                    pars.Add("client_secret", secretKey);
                    pars.Add("grant_type", "refresh_token");
                    // Посылаем параметры на сервер
                    // Может быть ответ в виде массива байт
                    webClient.Encoding = Encoding.UTF8;
                    var response = webClient.UploadValues(tokenUrl, pars);
                    string content = webClient.Encoding.GetString(response);
                    var model = JsonConvert.DeserializeObject<AccessTokenOKResponse>(content);
                    if (model.error != null)
                        return new BaseResponseSocial<AccessTokenOKResponse>(EnumResponseStatusSocial.Error, model.error_description);
                    return new BaseResponseSocial<AccessTokenOKResponse>(model);
                }
            }
            catch (Exception e)
            {
                return new BaseResponseSocial<AccessTokenOKResponse>(EnumResponseStatusSocial.Exception, e.Message);
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
    }
}
