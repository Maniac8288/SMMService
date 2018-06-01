namespace SMM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumn_PostIdOk_IN_Post : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "PostIdOK", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "PostIdOK");
        }
    }
}
