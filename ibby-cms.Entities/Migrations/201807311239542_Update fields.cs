namespace ibby_cms.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatefields : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PageContentEssence", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PageContentEssence", "Content", c => c.String());
        }
    }
}
