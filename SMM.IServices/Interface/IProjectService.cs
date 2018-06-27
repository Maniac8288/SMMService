using SMM.IServices.Models.Project;
using SMM.IServices.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.IServices.Interface
{
    public interface IProjectService
    {
        /// <summary>
        /// Создать проект
        /// </summary>
        /// <param name="name">Название проекта</param>
        /// <param name="userId">Ид создателя</param>
        /// <returns></returns>
        BaseResponse<int> CreateProject(string name, int userId);
        /// <summary>
        /// Получить проект
        /// </summary>
        /// <param name="id">Ид проекта</param>
        /// <returns></returns>
        BaseResponse<ProjectModel> GetProject(int id);
        /// <summary>
        /// Получить список проектов 
        /// </summary>
        /// <param name="userId">Ид пользователя </param>
        /// <returns></returns>
        List<ProjectModel> GetListProjects(int userId);
        /// <summary>
        /// Редактировать проект
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="model">Проект</param>
        /// <returns></returns>
        BaseResponse EditProject(int userId, ProjectModel model);
        /// <summary>
        /// Редактировать информацию о проекте
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResponse EditInfoProject(ProjectModel model);
        #region Группы 
        /// <summary>
        /// Установить группу
        /// </summary>
        /// <param name="projectId">Ид проекта</param>
        /// <param name="groupId">Ид группы</param>
        /// <returns></returns>
        BaseResponse SetGroup(int projectId, int? groupId);
        #endregion
    }
}
