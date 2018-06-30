namespace SMM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_IsFavorite_IsArhive_IN_Project : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "IsFavorite", c => c.Boolean(nullable: false));
            AddColumn("dbo.Projects", "IsArhive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "IsArhive");
            DropColumn("dbo.Projects", "IsFavorite");
        }
    }
}
