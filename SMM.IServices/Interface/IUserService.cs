using SMM.IServices.Enums;
using SMM.IServices.Models.Responses;
using SMM.IServices.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.IServices.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// Получить ключ доступа от вк
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        BaseResponse<string> GetAccessTokenVk(int userId);
        /// <summary>
        /// Получить ключ доступа от одноклассников
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        BaseResponse<string> GetAccessTokenOk(int userId);
        /// <summary>
        /// Устоновить ключ доступа от вк
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        BaseResponse SetAccessTokenVk(string accessToken, int userId);
        /// <summary>
        /// Проверить авторизован ли пользователь (вызывать методо после WebUser().UserId)
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <returns></returns>
        BaseResponse CheckAuthUser(int userId);
        /// <summary>
        /// Устоновить ключ доступа от одноклассников
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        BaseResponse SetAccessTokenOk(string accessToken, int userId);
        /// <summary>
        /// Установить пользователя в куки
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <returns></returns>
        void SetUserCookie(int userId);
        /// <summary>
        /// Проверка на существование пользователя
        /// </summary>
        /// <param name="socialId"></param>
        /// <returns></returns>
        BaseResponse<int> CheckUserWithVk(int socialId);
        /// <summary>
        /// Зарегистрировать пользователя от вк
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        BaseResponse<int> RegisterUserFromSocial(UserModel user);
        /// <summary>
        /// Проверить авторизован ли пользователь в социальной сети
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="social">Социальная сеть</param>
        /// <returns></returns>
        BaseResponse CheckAuthUserSocial(int userId, EnumSocial social);


        #region OK
        /// <summary>
        /// Проверка на существование пользователя
        /// </summary>
        /// <param name="socialId"></param>
        /// <returns></returns>
        BaseResponse<int> CheckUserWithOk(string id);
        #endregion
    }
}
