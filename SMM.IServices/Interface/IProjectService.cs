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
    }
}
