namespace SMM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameColumn_Title : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hashtags", "Title", c => c.String());
            DropColumn("dbo.Hashtags", "Titile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hashtags", "Titile", c => c.String());
            DropColumn("dbo.Hashtags", "Title");
        }
    }
}
