using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.IServices.Models.User
{
   public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? VkId { get; set; }
        public string OkId { get; set; }
        public string ImageUrl { get; set; }
    }
}
