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
                    var dateCreate = DateTime.Now;
                    var datePublic = dateCreate;
                    if (post.DatePublic != DateTime.MinValue)
                        datePublic = post.DatePublic;
                    var model = new Post()
                    {
                        Content = post.Content,
                        DateCreate = dateCreate,
                        DatePublic = datePublic,
                        ProjectId = post.ProjectId,
                        UserId = userId,
                        Status = (int)post.Status
                    };
                    db.Posts.Add(model);

                    if (post.Status == EnumStatusPost.Published)
                    {
                        post.DatePublic = datePublic;
                        post.DateCreate = dateCreate;
                        var okRes = PublicationOK(userId, post, project.GroupOK);
                        if (!okRes.IsSuccess)
                            return new BaseResponse<int>() { Status = okRes.Status, Message = okRes.Message };
                    }
                    db.SaveChanges();
                    return new BaseResponse<int>() { Status = 0, Message = "Успешно", Value = model.Id };
                }
            }
            catch (Exception e)
            {
                return new BaseResponse<int>(EnumResponseStatus.Exception, e.Message);
            }
        }
        /// <summary>
        /// Потвердить пост к отправке
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="postId">Id Постa</param>
        /// <returns></returns>
        public BaseResponse VerificationPost(int userId, int postId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var post = db.Posts.FirstOrDefault(x => x.Id == postId);
                    if (post == null)
                        return new BaseResponse(EnumResponseStatus.Error, "Пост не найден");
                    var group = db.Projects.FirstOrDefault(x => x.Id == post.ProjectId);
                    if (group == null)
                        return new BaseResponse(EnumResponseStatus.Error, "Проект поста не найдена");

                    post.Status = (int)EnumStatusPost.Confirmed;
                    db.SaveChanges();
                    var response = PublicationOK(userId, ConvertToPostModel(post), group.GroupOK);
                    if (response.IsSuccess)
                    {
                        post.PostIdOK = response.Value;
                        db.SaveChanges();
                    }
                    return response;
                }
            }
            catch (Exception e)
            {
                return new BaseResponse(EnumResponseStatus.Exception, e.Message);
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
        private BaseResponse<string> PublicationOK(int userId, PostModel post, string groupId)
        {
            var client = new OKService();
            var refresh_token = _userService.GetAccessTokenOk(userId);
            if (refresh_token.IsSuccess)
            {
                var accessToken = client.GetAccessTokenWithRefreshToken(refresh_token.Value);
                if (!accessToken.IsSuccess)
                    return new BaseResponse<string>() { Status = accessToken.Status, Message = accessToken.Message, };
                DateTime? datePublic = null;
                if (post.Status != EnumStatusPost.Published)
                    datePublic = post.DatePublic;
                var postResponse = client.Post(accessToken.Value.access_token, post.Content, groupId, datePublic);
                return new BaseResponse<string>()
                {
                    Status = postResponse.Status,
                    Message = postResponse.Message,
                    Value = postResponse.Value
                };
            }
            return new BaseResponse<string>()
            {
                Status = refresh_token.Status,
                Message = refresh_token.Message,
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
                Id = model.Id,
                Content = model.Content,
                DateCreate = model.DateCreate,
                DatePublic = model.DatePublic,
                ProjectId = model.ProjectId,
                UserId = model.UserId,
                Status = (EnumStatusPost)model.Status
            };
        }
        #endregion
    }
}
