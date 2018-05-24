using SMM.Data;
using SMM.Data.Models;
using SMM.IServices.Enums;
using SMM.IServices.Interface;
using SMM.IServices.Models.Project;
using SMM.IServices.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace SMM.Services
{
    public class ProjectService : IProjectService
    {
        /// <summary>
        /// Создать проект
        /// </summary>
        /// <param name="name">Название проекта</param>
        /// <param name="userId">Ид создателя</param>
        /// <returns></returns>
        public BaseResponse<int> CreateProject(string name, int userId)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (db.Projects.Any(x => x.Name == name && x.CreatorId == userId))
                        return new BaseResponse<int>(EnumResponseStatus.Error, "Проект с таким названием уже существуеет");
                    var project = new Project()
                    {
                        DateCreate = DateTime.Now,
                        CreatorId = userId,
                        Name = name,
                    };
                    db.Projects.Add(project);
                    db.SaveChanges();
                    return new BaseResponse<int>(EnumResponseStatus.Success, "Проект успешно создан", project.Id);
                }
            }
            catch (Exception e)
            {
                return new BaseResponse<int>(EnumResponseStatus.Exception, e.Message);
            }
        }

        /// <summary>
        /// Редактировать проект
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="model">Проект</param>
        /// <returns></returns>
        public BaseResponse EditProject(int userId, ProjectModel model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var project = db.Projects.FirstOrDefault(x => x.Id == model.Id);
                    if (project == null)
                        return new BaseResponse(EnumResponseStatus.Error, "Проект не найден");
                    if (project.CreatorId != userId)
                        return new BaseResponse(EnumResponseStatus.Error, "Вы не являетесь автором данного проекта");
                    project.GroupOK = model.GroupOK;
                    project.GroupVk = model.GroupVk;
                    project.Name = model.Name;        
                    db.SaveChanges();
                    return new BaseResponse();
                }
            }
            catch (Exception e)
            {
                return new BaseResponse(EnumResponseStatus.Exception, e.Message);
            }
        }
        /// <summary>
        /// Получить проект
        /// </summary>
        /// <param name="id">Ид проекта</param>
        /// <returns></returns>
        public BaseResponse<ProjectModel> GetProject(int id)
        {
            using (var db = new DataContext())
            {
                var project = db.Projects.FirstOrDefault(x => x.Id == id);
                if (project == null)
                    return new BaseResponse<ProjectModel>(EnumResponseStatus.Error, "Проект не был найден");
                var response = ConvertToProjectModel(project);
                return new BaseResponse<ProjectModel>(EnumResponseStatus.Success, response);
            }
        }
        /// <summary>
        /// Получить список проектов 
        /// </summary>
        /// <param name="userId">Ид пользователя </param>
        /// <returns></returns>
        public List<ProjectModel> GetListProjects(int userId)
        {
            using (var db = new DataContext())
            {
                return db.Projects.Where(x => x.CreatorId == userId).Select(ConvertToProjectModel).ToList();
            }
        }
        #region Конвертирование 
        private ProjectModel ConvertToProjectModel(Project m)
        {
            var path = System.Web.HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ProjectImage"]+m.Id+"/Image/");
            return new ProjectModel
            {
                Id = m.Id,
                CreatorId = m.CreatorId,
                GroupOK = m.GroupOK,
                DateCreate = m.DateCreate,
                GroupVk = m.GroupVk,
                Name = m.Name,
                ImageUrl = FileService.GetImageProject(path, m.Id)
            };
        }
        #endregion
    }
}
