namespace SMM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTable_HashTag : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostSocialsPublication", "SocialId", "dbo.Socials");
            DropForeignKey("dbo.PostSocialsPublication", "PostId", "dbo.Posts");
            CreateTable(
                "dbo.Hashtags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titile = c.String(),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.HashTagInPosts",
                c => new
                    {
                        HashtagId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HashtagId, t.PostId })
                .ForeignKey("dbo.Hashtags", t => t.HashtagId)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .Index(t => t.HashtagId)
                .Index(t => t.PostId);
            
            AddForeignKey("dbo.PostSocialsPublication", "SocialId", "dbo.Socials", "Id");
            AddForeignKey("dbo.PostSocialsPublication", "PostId", "dbo.Posts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostSocialsPublication", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostSocialsPublication", "SocialId", "dbo.Socials");
            DropForeignKey("dbo.Hashtags", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.HashTagInPosts", "PostId", "dbo.Posts");
            DropForeignKey("dbo.HashTagInPosts", "HashtagId", "dbo.Hashtags");
            DropIndex("dbo.HashTagInPosts", new[] { "PostId" });
            DropIndex("dbo.HashTagInPosts", new[] { "HashtagId" });
            DropIndex("dbo.Hashtags", new[] { "ProjectId" });
            DropTable("dbo.HashTagInPosts");
            DropTable("dbo.Hashtags");
            AddForeignKey("dbo.PostSocialsPublication", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PostSocialsPublication", "SocialId", "dbo.Socials", "Id", cascadeDelete: true);
        }
    }
}
