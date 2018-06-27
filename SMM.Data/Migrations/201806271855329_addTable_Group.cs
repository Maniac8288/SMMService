namespace SMM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTable_Group : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Projects", "GroupId", c => c.Int());
            CreateIndex("dbo.Projects", "GroupId");
            AddForeignKey("dbo.Projects", "GroupId", "dbo.Groups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Groups", "UserId", "dbo.Users");
            DropIndex("dbo.Groups", new[] { "UserId" });
            DropIndex("dbo.Projects", new[] { "GroupId" });
            DropColumn("dbo.Projects", "GroupId");
            DropTable("dbo.Groups");
        }
    }
}
