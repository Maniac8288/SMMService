namespace SMM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumn_OKId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "OkId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "OkId");
        }
    }
}
