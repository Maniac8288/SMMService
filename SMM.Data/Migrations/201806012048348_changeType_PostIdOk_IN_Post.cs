namespace SMM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeType_PostIdOk_IN_Post : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "PostIdOK", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "PostIdOK", c => c.Int(nullable: false));
        }
    }
}
