namespace SMM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumn_AccessTokenVK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AccessTokenVk", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "AccessTokenVk");
        }
    }
}
