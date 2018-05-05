namespace SMM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumn_AccessTokenOk_IN_User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AccessTokenOk", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "AccessTokenOk");
        }
    }
}
