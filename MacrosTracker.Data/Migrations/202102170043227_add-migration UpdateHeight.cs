namespace MacrosTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationUpdateHeight : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUser", "Height", c => c.Int(nullable: false));
            DropColumn("dbo.ApplicationUser", "HeightInInches");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUser", "HeightInInches", c => c.Int(nullable: false));
            DropColumn("dbo.ApplicationUser", "Height");
        }
    }
}
