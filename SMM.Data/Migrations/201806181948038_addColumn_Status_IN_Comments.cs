namespace SMM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumn_Status_IN_Comments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Status");
        }
    }
}
