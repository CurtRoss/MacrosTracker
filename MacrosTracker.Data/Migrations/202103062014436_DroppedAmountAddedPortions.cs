namespace MacrosTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DroppedAmountAddedPortions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipe", "HowManyPortions", c => c.Int(nullable: false));
            DropColumn("dbo.Meal", "FoodId");
            DropColumn("dbo.FoodItem", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FoodItem", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Meal", "FoodId", c => c.Int(nullable: false));
            DropColumn("dbo.Recipe", "HowManyPortions");
        }
    }
}
