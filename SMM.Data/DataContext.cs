using SMM.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection") { }

        /// <summary>
        /// Таблица пользователей
        /// </summary>
        public DbSet<User> Users { get; set; }

   

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            #region User


            #endregion

           

            base.OnModelCreating(mb);
        }
    }
}
