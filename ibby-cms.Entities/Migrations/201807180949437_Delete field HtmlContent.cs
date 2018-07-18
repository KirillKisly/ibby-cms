namespace ibby_cms.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletefieldHtmlContent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PageContentEssence", "Header", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.PageContentEssence", "HtmlContent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PageContentEssence", "HtmlContent", c => c.String());
            AlterColumn("dbo.PageContentEssence", "Header", c => c.String());
        }
    }
}
