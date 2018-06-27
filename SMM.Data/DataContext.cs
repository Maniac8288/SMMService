using SMM.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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
        /// <summary>
        /// Таблица пользователей одноклассников
        /// </summary>
        public DbSet<UserOk> UsersOk { get; set; }
        /// <summary>
        /// Таблица пользователей Вконтакте
        /// </summary>
        public DbSet<UserVk> UsersVk { get; set; }
        /// <summary>
        /// Таблица с постами
        /// </summary>
        public DbSet<Post> Posts { get; set; }
        /// <summary>
        /// Таблица с проектами
        /// </summary>
        public DbSet<Project> Projects { get; set; }
        /// <summary>
        /// Таблица с соц сетями
        /// </summary>
        public DbSet<Social> Socials { get; set; }
        /// <summary>
        /// Таблица с комментариями
        /// </summary>
        public DbSet<Comment> Comments { get; set; }
        /// <summary>
        /// Таблица хэштэгов
        /// </summary>
        public DbSet<Hashtag> Hashtags { get; set; }
        /// <summary>
        /// Группы
        /// </summary>
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            #region Users
            mb.Entity<User>().HasRequired(_ => _.UserOk).WithRequiredPrincipal(x => x.User);
            mb.Entity<User>().HasRequired(_ => _.UserVk).WithRequiredPrincipal(x => x.User);

            mb.Entity<UserOk>().Property(_ => _.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            mb.Entity<UserOk>().HasKey(_ => _.UserId);

            mb.Entity<UserVk>().Property(_ => _.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            mb.Entity<UserVk>().HasKey(_ => _.UserId);

            mb.Entity<Group>().HasRequired(x => x.User).WithMany(x => x.Groups).HasForeignKey(x => x.UserId);
            #endregion

            #region Проекты 
            mb.Entity<Project>().HasRequired(x => x.User).WithMany(x => x.Projects).HasForeignKey(x => x.CreatorId);
            mb.Entity<Project>().HasOptional(x => x.Group).WithMany(x => x.Projects).HasForeignKey(x => x.GroupId);
            #endregion

            #region Посты
            mb.Entity<Post>().HasRequired(x => x.User).WithMany(x => x.Posts).HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            mb.Entity<Post>().HasRequired(x => x.Project).WithMany(x => x.Posts).HasForeignKey(x => x.ProjectId);
            #endregion

            #region Social
            mb.Entity<Social>().Property(_ => _.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            mb.Entity<Social>().HasKey(_ => _.Id);

            mb.Entity<Social>().HasMany(p => p.Posts).WithMany(c => c.Socials)
         .Map(t => t.MapLeftKey("SocialId").MapRightKey("PostId").ToTable("PostSocialsPublication"));
            #endregion

            #region Comment
            mb.Entity<Comment>().HasRequired(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            mb.Entity<Comment>().HasRequired(x => x.Post).WithMany(x => x.Comments).HasForeignKey(x => x.PostId);
            #endregion

            #region Хэштэги
            mb.Entity<Hashtag>().HasRequired(x => x.Project).WithMany(x => x.Hashtags).HasForeignKey(x => x.ProjectId);
            mb.Entity<Hashtag>().HasMany(x => x.Posts).WithMany(x => x.Hashtags)
                        .Map(t => t.MapLeftKey("HashtagId").MapRightKey("PostId").ToTable("HashTagInPosts"));
            #endregion
            mb.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(mb);
        }
    }
}
