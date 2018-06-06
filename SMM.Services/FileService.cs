using SMM.IServices.Enums;
using SMM.IServices.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace SMM.Services
{
    public static class FileService
    {
        /// <summary>
        /// Загрузить изображения для проекта
        /// </summary>
        /// <param name="path">Путь</param>
        /// <param name="image">Изображение</param>
        /// <returns></returns>
        public static BaseResponse UploadImageProject(string path, HttpPostedFileBase image)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //string extension = Path.GetExtension(image.FileName);
                image.SaveAs(path + "Avatar.png");
                return new BaseResponse(EnumResponseStatus.Success, "Файл успешно загружен");
            }
            catch (Exception e)
            {
                return new BaseResponse(EnumResponseStatus.Exception, e.Message);
            }
        }

        /// <summary>
        /// Получить ссылку для картинки проекта
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetImageProject(string path, int id)
        {
            if (Directory.Exists(path) && Directory.EnumerateFileSystemEntries(path).Any())
                return WebConfigurationManager.AppSettings["ProjectImage"].Remove(0, 1) + id + "/Image/" + "Avatar.png";
            return WebConfigurationManager.AppSettings["pathNoneImage"];
        }
        /// <summary>
        /// Загрузить изображения для поста
        /// </summary>
        /// <param name="path">Путь</param>
        /// <param name="image">Изображение</param>
        /// <returns></returns>
        public static BaseResponse UploadPost(string path, HttpPostedFileBase image)
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                image.SaveAs(path + image.FileName);
                return new BaseResponse(EnumResponseStatus.Success, "Файл успешно загружен");
            }
            catch (Exception e)
            {
                return new BaseResponse(EnumResponseStatus.Exception, e.Message);
            }
        }
        /// <summary>
        /// Получить ссылку для картинки проекта
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<string> GetListImagePostForUpload(string path)
        {
            if (Directory.Exists(path) && Directory.EnumerateFileSystemEntries(path).Any())
            {
                string[] files = Directory.GetFiles(path); // путь к папке
                var resposne = new List<string>();
                for (int i = 0; i < files.Length; i++)
                {
                    resposne.Add(files[i]);
                }
                return resposne;
            }
            return new List<string>();
        }
        /// <summary>
        /// Получить ссылку для картинки поста
        /// </summary>
        /// <param name="path">Путь</param>
        /// <param name="postId">Ид поста</param>
        /// <returns></returns>
        public static List<string> GetListImagePostForView(string path, int postId)
        {
            if (Directory.Exists(path) && Directory.EnumerateFileSystemEntries(path).Any())
            {
                string[] files = Directory.GetFiles(path); // путь к папке
                var resposne = new List<string>();
                for (int i = 0; i < files.Length; i++)
                {
                    var fileName = Path.GetFileName(files[i]);
                    resposne.Add(WebConfigurationManager.AppSettings["Post"].Remove(0, 1) + postId + "/Image/" + fileName);
                }
                return resposne;
            }
            return new List<string>();
        }
    }
}
