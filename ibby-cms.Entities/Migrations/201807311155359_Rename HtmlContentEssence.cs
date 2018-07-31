namespace ibby_cms.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameHtmlContentEssence : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PageHtmlContentEssence", newName: "HtmlContentEssence");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.HtmlContentEssence", newName: "PageHtmlContentEssence");
        }
    }
}
