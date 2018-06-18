namespace SMM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumn_DateCreate_IN_Comment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "DateCreate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "DateCreate");
        }
    }
}
