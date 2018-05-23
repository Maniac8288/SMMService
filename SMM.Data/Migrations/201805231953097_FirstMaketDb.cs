namespace SMM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMaketDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        DatePublic = c.DateTime(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatorId = c.Int(nullable: false),
                        GroupVk = c.String(),
                        GroupOK = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId, cascadeDelete: true)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.UserOks",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        OkId = c.String(),
                        AccessToken = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserVks",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        VkId = c.Int(),
                        AccessToken = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Socials",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostSocialsPublication",
                c => new
                    {
                        SocialId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SocialId, t.PostId })
                .ForeignKey("dbo.Socials", t => t.SocialId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.SocialId)
                .Index(t => t.PostId);
            
            DropColumn("dbo.Users", "VkId");
            DropColumn("dbo.Users", "OkId");
            DropColumn("dbo.Users", "AccessTokenVk");
            DropColumn("dbo.Users", "AccessTokenOk");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "AccessTokenOk", c => c.String());
            AddColumn("dbo.Users", "AccessTokenVk", c => c.String());
            AddColumn("dbo.Users", "OkId", c => c.String());
            AddColumn("dbo.Users", "VkId", c => c.Int());
            DropForeignKey("dbo.Posts", "UserId", "dbo.Users");
            DropForeignKey("dbo.PostSocialsPublication", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostSocialsPublication", "SocialId", "dbo.Socials");
            DropForeignKey("dbo.Posts", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.UserVks", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserOks", "UserId", "dbo.Users");
            DropIndex("dbo.PostSocialsPublication", new[] { "PostId" });
            DropIndex("dbo.PostSocialsPublication", new[] { "SocialId" });
            DropIndex("dbo.UserVks", new[] { "UserId" });
            DropIndex("dbo.UserOks", new[] { "UserId" });
            DropIndex("dbo.Projects", new[] { "CreatorId" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.Posts", new[] { "ProjectId" });
            DropTable("dbo.PostSocialsPublication");
            DropTable("dbo.Socials");
            DropTable("dbo.UserVks");
            DropTable("dbo.UserOks");
            DropTable("dbo.Projects");
            DropTable("dbo.Posts");
        }
    }
}
