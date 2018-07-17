namespace ibby_cms.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsPublishedfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PageContentEssence", "IsPublished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PageContentEssence", "IsPublished");
        }
    }
}
