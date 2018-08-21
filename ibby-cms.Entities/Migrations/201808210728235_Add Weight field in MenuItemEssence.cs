namespace ibby_cms.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWeightfieldinMenuItemEssence : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuItemEssence", "Weight", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuItemEssence", "Weight");
        }
    }
}
