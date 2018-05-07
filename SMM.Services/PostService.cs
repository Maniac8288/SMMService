using SMM.Data;
using SMM.IServices.Enums;
using SMM.IServices.Interface;
using SMM.IServices.Models.Post;
using SMM.IServices.Models.Responses;
using SMM.Social.Models.BaseResponse;
using SMM.Social.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Services
{
    public class PostService : IPostService
    {
        private IUserService _userService = new UserService();
      
        /// <summary>
        /// Опубликовать пост
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="post">Пост</param>
        /// <returns></returns>
        public BaseResponse Publication(int userId, PostModel post)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var okRes = PublicationOK(userId, post, "59033221267498");
                    return okRes;
                }
            }
            catch (Exception e)
            {
                return new BaseResponse(EnumResponseStatus.Exception, e.Message);
            }
        }
        #region Одкноклассники 
        private BaseResponse PublicationOK(int userId, PostModel post, string groupId)
        {
            var client = new OKService();
            var refresh_token = _userService.GetAccessTokenOk(userId);
            if (refresh_token.IsSuccess)
            {
                var accessToken = client.GetAccessTokenWithRefreshToken(refresh_token.Value);
                if (!accessToken.IsSuccess)
                    return ConvertBaseResponseSocial(accessToken);
                var postResponse = client.Post(accessToken.Value.access_token, post.Content, groupId);

                return ConvertBaseResponseSocial(postResponse);
            }
            return new BaseResponse()
            {
                Status = refresh_token.Status,
                Message = refresh_token.Message
            };
        }
        #endregion

        #region Конвертация
        private BaseResponse ConvertBaseResponseSocial(BaseResponseSocial model)
        {
            return new BaseResponse()
            {
                Status = model.Status,
                Message = model.Message
            };
        }
        #endregion
    }
}
