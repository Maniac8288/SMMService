using SMM.Data;
using SMM.Data.Models;
using SMM.IServices.Enums;
using SMM.IServices.Interface;
using SMM.IServices.Models.Responses;
using SMM.IServices.Models.User;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using SMM.IServices.Models.User.Group;

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
                    var user = db.Users.Include(x=>x.UserVk).FirstOrDefault(x => x.Id == userId);
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
                    var user = db.Users.Include(x => x.UserOk).FirstOrDefault(x => x.Id == userId);
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
                    var user = db.Users.Include(x=>x.UserVk).FirstOrDefault(x => x.Id == userId);
                    if (user == null)
                        return new BaseResponse<string>(EnumResponseStatus.Error, "Пользователь не найден");
                    if (user.UserVk == null)
                        return new BaseResponse<string>(EnumResponseStatus.SocialNotExist, "Пользователь не авторизован через ВК");
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
                    var user = db.Users.Include(x => x.UserOk).FirstOrDefault(x => x.Id == userId);
                    if (user == null)
                        return new BaseResponse<string>(EnumResponseStatus.Error, "Пользователь не найден");
                    if (user.UserOk == null)
                        return new BaseResponse<string>(EnumResponseStatus.SocialNotExist, "Пользователь не авторизован через ОК");
                    return new BaseResponse<string>(EnumResponseStatus.Success, "Токен успешно получен", user.UserOk.AccessToken);
                }
            }
            catch (Exception e)
            {
                return new BaseResponse<string>(EnumResponseStatus.Exception, e.Message);
            }
        }
        /// <summary>
        /// Получить ключ ид пользователя в одноклассниках
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public BaseResponse<string> GetUidUserOK(int userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var user = db.Users.Include(x => x.UserOk).FirstOrDefault(x => x.Id == userId);
                    if (user == null)
                        return new BaseResponse<string>(EnumResponseStatus.Error, "Пользователь не найден");
                    if (user.UserOk == null)
                        return new BaseResponse<string>(EnumResponseStatus.SocialNotExist, "Пользователь не авторизован через ОК");
                    return new BaseResponse<string>(EnumResponseStatus.Success, "Ид  получено", user.UserOk.OkId);
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
        /// <summary>
        /// Проверить авторизован ли пользователь (вызывать методо после WebUser().UserId)
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <returns></returns>
        public BaseResponse CheckAuthUser(int userId)
        {
            if (userId == 0)
                return new BaseResponse(EnumResponseStatus.Error, "Вы не авторизованы");
            return new BaseResponse(EnumResponseStatus.Success);
        }
        /// <summary>
        /// Проверить авторизован ли пользователь в социальной сети
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="social">Социальная сеть</param>
        /// <returns></returns>
        public BaseResponse CheckAuthUserSocial(int userId, EnumSocial social)
        {
            using (var db = new DataContext())
            {
                var user = db.Users.Include(x => x.UserOk).Include(x => x.UserVk).FirstOrDefault(x => x.Id == userId);
                if (user == null)
                    return new BaseResponse(EnumResponseStatus.Error, "Пользователь не найден");

                switch (social)
                {
                    case EnumSocial.vk:
                        if (user.UserVk == null)
                            return new BaseResponse(EnumResponseStatus.SocialNotExist, "Пользователь не авторизован через VK");
                        break;
                    case EnumSocial.ok:
                        if (user.UserOk == null)
                            return new BaseResponse(EnumResponseStatus.SocialNotExist, "Пользователь не авторизован через OK");
                        break;
                }

                return new BaseResponse(EnumResponseStatus.Success);
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


        #region Работа с группами пользователя
        /// <summary>
        /// Создать группы
        /// </summary>
        /// <param name="name">Название группы</param>
        /// <param name="userId">Ид создателя</param>
        /// <returns></returns>
        public BaseResponse CreateGroup(string name, int userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (db.Groups.Any(x => x.Name == name && x.UserId == userId))
                        return new BaseResponse<int>(EnumResponseStatus.ValidationError, "Группа с таким названием уже существуеет");
                    var grp = new Group()
                    {                       
                        Name = name,
                        UserId = userId,                        
                    };
                    db.Groups.Add(grp);
                    db.SaveChanges();
                    return new BaseResponse(EnumResponseStatus.Success, "Группа успешно создан");
                }
            }
            catch (Exception e)
            {
                return new BaseResponse(EnumResponseStatus.Exception, e.Message);
            }
        }
        /// <summary>
        /// Получить список груп пользователя
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <returns></returns>
        public List<GroupModel> GetGroups(int userId)
        {
            using(var db = new DataContext())
            {
                return db.Groups.Where(x => x.UserId == userId).Select(ConvertGroup).ToList();
            }
        }

        #region Конвертирование 
        private GroupModel ConvertGroup(Group model)
        {
            return new GroupModel
            {
                Id = model.Id,
                Name = model.Name,
                UserId = model.UserId
            };
        }
        #endregion
        #endregion
    }
}
