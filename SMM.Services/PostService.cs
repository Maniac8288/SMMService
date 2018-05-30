using SMM.Data;
using SMM.Data.Models;
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
        public BaseResponse<int> Publication(int userId, PostModel post)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var project = db.Projects.FirstOrDefault(x => x.Id == post.ProjectId);
                    if (project == null)
                        return new BaseResponse<int>(EnumResponseStatus.Error, "Проект не найден");
                    var okRes = PublicationOK(userId, post, project.GroupOK);
                    if (okRes.IsSuccess)
                    {
                        var model = new Post()
                        {
                            Content = post.Content,
                            DateCreate = DateTime.Now,
                            DatePublic = DateTime.Now,
                            ProjectId = post.ProjectId,
                            UserId = userId
                         
                        };
                        db.Posts.Add(model);
                        db.SaveChanges();
                        return new BaseResponse<int>() { Status = okRes.Status, Message = okRes.Message, Value = model.Id };
                    }
                    return new BaseResponse<int>() { Status = okRes.Status, Message = okRes.Message };
                }
            }
            catch (Exception e)
            {
                return new BaseResponse<int>(EnumResponseStatus.Exception, e.Message);
            }
        }

        /// <summary>
        /// Получить пост по ид
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public PostModel GetPostById(int postId)
        {

            using (var db = new DataContext())
            {
                var post = db.Posts.FirstOrDefault(x => x.Id == postId);
                if (post == null)
                    return new PostModel();
                return ConvertToPostModel(post);
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
        private PostModel ConvertToPostModel(Post model)
        {
            return new PostModel()
            {
                Content = model.Content,
                DateCreate =model.DateCreate,
                DatePublic = model.DatePublic,
                ProjectId = model.ProjectId,
                UserId = model.UserId
            };
        }
        #endregion
    }
}
