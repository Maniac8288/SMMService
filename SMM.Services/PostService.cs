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
using System.Data.Entity;
using System.Web;
using System.Web.Configuration;
using SMM.Social.Models.OK.Publication;
using SMM.Social.Models.OK.Photos;

namespace SMM.Services
{
    public class PostService : IPostService
    {
        private IUserService _userService = new UserService();

        #region GetPost 
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
        /// <summary>
        /// Получить посты по ид проекта
        /// </summary>
        /// <param name="projectId">Ид проекта</param>
        /// <returns></returns>
        public List<PostModel> GetPostsProject(int projectId)
        {
            using (var db = new DataContext())
            {
                var project = db.Projects.Include(x => x.Posts).FirstOrDefault(x => x.Id == projectId);
                if (project == null)
                    return new List<PostModel>();
                return project.Posts.Select(ConvertToPostModel).OrderByDescending(x => x.DateCreate).ToList();
            }
        }

        /// <summary>
        /// Получить посты по ид проекта для календаря
        /// </summary>
        /// <param name="projectId">Ид проекта</param>
        /// <returns></returns>
        public List<PostCalendarModel> GetPostsForCalendar(int projectId)
        {
            using (var db = new DataContext())
            {
                var project = db.Projects.Include(x => x.Posts).FirstOrDefault(x => x.Id == projectId);
                if (project == null)
                    return new List<PostCalendarModel>();
                return project.Posts.Select(ConvertToPostCalendar).ToList();
            }
        }
        #endregion

        #region Публикация
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
                    db.SaveChanges();
                    var path = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["Post"] + model.Id + "/Image/");
                    FileService.UploadPost(path, post.ImageFile);
                    if (post.Status == EnumStatusPost.Published)
                    {
                        post.DatePublic = datePublic;
                        post.DateCreate = dateCreate;
                        post.Id = model.Id;
                        var okRes = PublicationOK(userId, post, project.GroupOK);
                        if (!okRes.IsSuccess)
                            return new BaseResponse<int>() { Status = okRes.Status, Message = okRes.Message };
                    }

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
        #endregion

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

                var attachmentModel = new AttachmentModel();
                var media = new List<MediaModel>
                {
                    new MediaModel {
                        type = "text",
                        text = post.Content
                    }
                };

                if (post.Status != EnumStatusPost.Published)
                    attachmentModel.publishAt = ((post.DatePublic)).ToString("yyyy-MM-dd HH':'mm':'ss");
                if (post.ImageFile != null)
                {
                    var uploadUrl = client.GetUploadUrl(accessToken.Value.access_token, groupId);
                    var files = FileService.GetListImagePostForUpload(HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["Post"] + post.Id + "/Image/"));
                    var response = client.UploadPhoto(uploadUrl.Value.upload_url, files[0], uploadUrl.Value.photo_ids[0]);
                    if (response.IsSuccess)
                    {
                        media.Add(new MediaModel
                        {
                            type = "photo",
                            list = new List<MediaPhotoModel>()
                            {
                                new MediaPhotoModel()
                                {
                                    id = response.Value
                                }
                            }
                        });
                    }
                }
                attachmentModel.media = media;
                var postResponse = client.Post(accessToken.Value.access_token, groupId, attachmentModel);
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
            var path = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["Post"] + model.Id + "/Image/");
            return new PostModel()
            {
                Id = model.Id,
                Content = model.Content,
                DateCreate = model.DateCreate,
                DatePublic = model.DatePublic,
                ProjectId = model.ProjectId,
                UserId = model.UserId,
                Status = (EnumStatusPost)model.Status,
                ImagesUrl = FileService.GetListImagePostForView(path, model.Id)
            };
        }
        private PostCalendarModel ConvertToPostCalendar(Post model)
        {
            return new PostCalendarModel()
            {
                Id = model.Id,
                DatePublic = model.DatePublic,
                Name = "Пока нету название у постов",
                Soical = EnumSocial.ok.ToString()
            };
        }
        #endregion
    }
}
