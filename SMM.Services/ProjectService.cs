using SMM.Data;
using SMM.Data.Models;
using SMM.IServices.Enums;
using SMM.IServices.Interface;
using SMM.IServices.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        DateCreate =  DateTime.Now,
                        CreatorId = userId,
                        Name = name,
                    };
                    db.Projects.Add(project);
                    db.SaveChanges();
                    return new BaseResponse<int>(EnumResponseStatus.Success, "Проект успешно создан",project.Id);
                }
            }
            catch (Exception e)
            {
                return new BaseResponse<int>(EnumResponseStatus.Exception, e.Message);
            }
        }
    }
}
