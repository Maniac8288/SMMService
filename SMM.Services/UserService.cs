using SMM.Data;
using SMM.Data.Models;
using SMM.IServices.Enums;
using SMM.IServices.Interface;
using SMM.IServices.Models.Responses;
using SMM.IServices.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SMM.Services
{
    /// <summary>
    /// Сервис для работы с пользователем
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Устоновить ключ доступа от вк
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public BaseResponse SetAccessTokenVk(string accessToken, int userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var user = db.Users.FirstOrDefault(x => x.Id == userId);
                    if (user == null)
                        return new BaseResponse(EnumResponseStatus.Error, "Пользователь не найден");
                    if (user.UserVk.AccessToken != accessToken)
                    {
                        user.UserVk.AccessToken = accessToken;
                        db.SaveChanges();
                    }
                    return new BaseResponse(EnumResponseStatus.Success, "Токен успешно установлен");
                }
            }
            catch (Exception e)
            {
                return new BaseResponse(EnumResponseStatus.Exception, e.Message);
            }
        }
        /// <summary>
        /// Устоновить ключ доступа от одноклассников
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public BaseResponse SetAccessTokenOk(string accessToken, int userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var user = db.Users.FirstOrDefault(x => x.Id == userId);
                    if (user == null)
                        return new BaseResponse(EnumResponseStatus.Error, "Пользователь не найден");
                    if (user.UserOk.AccessToken != accessToken)
                    {
                        user.UserOk.AccessToken = accessToken;
                        db.SaveChanges();
                    }
                    return new BaseResponse(EnumResponseStatus.Success, "Токен успешно установлен");
                }
            }
            catch (Exception e)
            {
                return new BaseResponse(EnumResponseStatus.Exception, e.Message);
            }
        }
        /// <summary>
        /// Получить ключ доступа от вк
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public BaseResponse<string> GetAccessTokenVk(int userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var user = db.Users.FirstOrDefault(x => x.Id == userId);
                    if (user == null)
                        return new BaseResponse<string>(EnumResponseStatus.Error, "Пользователь не найден");

                    return new BaseResponse<string>(EnumResponseStatus.Success, "Токен успешно получен", user.UserVk.AccessToken);
                }
            }
            catch (Exception e)
            {
                return new BaseResponse<string>(EnumResponseStatus.Exception, e.Message);
            }
        }
        /// <summary>
        /// Получить ключ доступа от одноклассников
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public BaseResponse<string> GetAccessTokenOk(int userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var user = db.Users.FirstOrDefault(x => x.Id == userId);
                    if (user == null)
                        return new BaseResponse<string>(EnumResponseStatus.Error, "Пользователь не найден");

                    return new BaseResponse<string>(EnumResponseStatus.Success, "Токен успешно получен", user.UserOk.AccessToken);
                }
            }
            catch (Exception e)
            {
                return new BaseResponse<string>(EnumResponseStatus.Exception, e.Message);
            }
        }
        /// <summary>
        /// Установить пользователя в куки
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <returns></returns>
        public void SetUserCookie(int userId)
        {
            HttpCookie cookieReq = HttpContext.Current.Request.Cookies["User"];
            if (string.IsNullOrWhiteSpace(cookieReq?.Value))
            {
                HttpCookie aCookie = new HttpCookie("User")
                {
                    Value = userId.ToString(),
                    Expires = DateTime.Now.AddDays(30)
                };
                HttpContext.Current.Response.Cookies.Add(aCookie);
            }
            else
            {
                var cookieUser = int.Parse(cookieReq.Value);
                if (cookieUser != userId)
                {
                    cookieReq.Value = userId.ToString();
                    HttpContext.Current.Response.Cookies.Add(cookieReq);
                }
            }
        }
        /// <summary>
        /// Зарегистрировать пользователя от вк
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public BaseResponse<int> RegisterUserFromSocial(UserModel user)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var userModel = new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ImageUrl = user.ImageUrl
                    };
                    db.Users.Add(userModel);
                    db.SaveChanges();
                    if (user.VkId != null)
                    {
                        var userVk = new UserVk()
                        {
                            UserId = userModel.Id,
                            VkId = user.VkId
                        };
                        db.UsersVk.Add(userVk);
                    }
                    if (!string.IsNullOrEmpty(user.OkId))
                    {
                        var userOk = new UserOk()
                        {
                            UserId = userModel.Id,
                            OkId = user.OkId
                        };
                        db.UsersOk.Add(userOk);
                    }
                    db.SaveChanges();
                    return new BaseResponse<int>(EnumResponseStatus.Success, "Пользователь успешно зарегистрирован", userModel.Id);
                }
            }
            catch (Exception e)
            {
                return new BaseResponse<int>(EnumResponseStatus.Exception, e.Message);
            }
        }
        #region VK

        /// <summary>
        /// Проверка на существование пользователя
        /// </summary>
        /// <param name="socialId"></param>
        /// <returns></returns>
        public BaseResponse<int> CheckUserWithVk(int socialId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var user = db.UsersVk.FirstOrDefault(x => x.VkId == socialId);
                    if (user == null)
                    {
                        return new BaseResponse<int>(EnumResponseStatus.Error, "Пользователь не найден");
                    }
                    else
                        return new BaseResponse<int>(user.UserId);
                }
            }
            catch (Exception e)
            {
                return new BaseResponse<int>(EnumResponseStatus.Exception, e.Message);
            }
        }

        #endregion

        #region OK
        /// <summary>
        /// Проверка на существование пользователя
        /// </summary>
        /// <param name="socialId"></param>
        /// <returns></returns>
        public BaseResponse<int> CheckUserWithOk(string id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var user = db.UsersOk.FirstOrDefault(x => x.OkId == id);
                    if (user == null)
                    {
                        return new BaseResponse<int>(EnumResponseStatus.Error, "Пользователь не найден");
                    }
                    else
                        return new BaseResponse<int>(user.UserId);
                }
            }
            catch (Exception e)
            {
                return new BaseResponse<int>(EnumResponseStatus.Exception, e.Message);
            }
        }

        #endregion
    }
}
