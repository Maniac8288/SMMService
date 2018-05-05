using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Social.Models.BaseResponse
{
    /// <summary>
    /// Базовый ответ сервера
    /// </summary>
    public class BaseResponseSocial
    {
        public BaseResponseSocial() { }

        public BaseResponseSocial(EnumResponseStatusSocial status, string message)
        {
            // подозреваю, что в JS вернется строка если не привести к целому
            Status = (int)status;
            Message = message;
        }

        public BaseResponseSocial(EnumResponseStatusSocial status)
        {
            Status = (int)status;
            Message = status == 0
                ? "Запрос успешно выполнен."
                : "Ошибка при выполнении запроса.";
        }
        public int Status { get; set; }

        public string Message { get; set; }

        public bool IsSuccess => Status == (int)EnumResponseStatusSocial.Success;
    }

    public class BaseResponseSocial<T> : BaseResponseSocial
    {
        public BaseResponseSocial() { }

        public BaseResponseSocial(EnumResponseStatusSocial status, string message) : base(status, message) { }

        public BaseResponseSocial(EnumResponseStatusSocial status) : base(status) { }

        public BaseResponseSocial(T value)
        {
            Status = 0;
            Message = "Запрос успешно выполнен.";
            Value = value;
        }

        public BaseResponseSocial(EnumResponseStatusSocial status, T value) : base(status)
        {
            Value = value;
        }

        public BaseResponseSocial(EnumResponseStatusSocial status, string message, T value) : base(status)
        {
            Message = message;
            Value = value;
        }

        public T Value { get; set; }
    }
}
