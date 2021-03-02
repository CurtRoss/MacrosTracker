namespace MacrosTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingRecipes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipe", "CreatedUtc", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipe", "CreatedUtc");
        }
    }
}
