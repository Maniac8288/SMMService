namespace SMM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumn_Statis_IN_Post : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Status");
        }
    }
}
