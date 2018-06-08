namespace SMM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumns_MainInfo_AdditinalInfo_JSonCards_IN_Project : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "MainInfo", c => c.String());
            AddColumn("dbo.Projects", "AdditionalInfo", c => c.String());
            AddColumn("dbo.Projects", "JsonCards", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "JsonCards");
            DropColumn("dbo.Projects", "AdditionalInfo");
            DropColumn("dbo.Projects", "MainInfo");
        }
    }
}
