namespace SMM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumn_Status_IN_Project : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Status");
        }
    }
}
