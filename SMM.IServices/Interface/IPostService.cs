using SMM.IServices.Enums;
using SMM.IServices.Models.Post;
using SMM.IServices.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.IServices.Interface
{
    public interface IPostService
    {
        /// <summary>
        /// Опубликовать пост
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="post">Пост</param>
        /// <returns></returns>
        BaseResponse<int> Publication(int userId, PostModel post);
        /// <summary>
        /// Получить пост по ид
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        PostModel GetPostById(int postId);
        /// <summary>
        /// Потвердить пост к отправке
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="postId">Id Постa</param>
        /// <returns></returns>
        BaseResponse VerificationPost(int userId, int postId);
        /// <summary>
        /// Получить посты по ид проекта
        /// </summary>
        /// <param name="projectId">Ид проекта</param>
        /// <returns></returns>
        List<PostModel> GetPostsProject(int projectId);
        /// <summary>
        /// Получить посты по ид проекта для календаря
        /// </summary>
        /// <param name="projectId">Ид проекта</param>
        /// <returns></returns>
        List<PostCalendarModel> GetPostsForCalendar(int projectId);
        /// <summary>
        /// Отправить комментарий к посту 
        /// </summary>
        /// <param name="userId">Ид пользователя который оставляет комментарий</param>
        /// <param name="postId">Ид поста которому принадлежит комментарий</param>
        /// <param name="content">Содержимое комментария</param>
        /// <returns></returns>
        BaseResponse<CommentModel> SendComment(int userId, int postId, string content, EnumStatusComment status);
        #region Редактирование поста
        /// <summary>
        /// Сменить содержимое поста
        /// </summary>
        /// <param name="userId">Ид пользователя который хочет изменить контнет</param>
        /// <param name="postId">Ид поста</param>
        /// <param name="content">Новое содержимое поста</param>
        /// <returns></returns>
        BaseResponse ChangeContent(int userId, int postId, string content);
        /// <summary>
        /// Удалить пост
        /// </summary>
        /// <param name="userId">Ид пользователя который хочет изменить контнет</param>
        /// <param name="postId">Ид поста</param>
        /// <returns></returns>
        BaseResponse DeletePost(int userId, int postId);
        #endregion
    }
}
