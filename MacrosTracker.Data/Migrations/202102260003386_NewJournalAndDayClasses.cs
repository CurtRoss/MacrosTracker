namespace MacrosTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewJournalAndDayClasses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Day",
                c => new
                    {
                        DayId = c.Int(nullable: false, identity: true),
                        DateOfEntry = c.DateTime(nullable: false),
                        TotalCalories = c.Int(nullable: false),
                        TotalFats = c.Int(nullable: false),
                        TotalCarbs = c.Int(nullable: false),
                        TotalProteins = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DayId);
            
            CreateTable(
                "dbo.JournalEntry",
                c => new
                    {
                        JournalEntryId = c.Int(nullable: false, identity: true),
                        DayId = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        FoodItem_FoodId = c.Int(),
                        Meal_MealId = c.Int(),
                    })
                .PrimaryKey(t => t.JournalEntryId)
                .ForeignKey("dbo.Day", t => t.DayId, cascadeDelete: true)
                .ForeignKey("dbo.FoodItem", t => t.FoodItem_FoodId)
                .ForeignKey("dbo.Meal", t => t.Meal_MealId)
                .Index(t => t.DayId)
                .Index(t => t.FoodItem_FoodId)
                .Index(t => t.Meal_MealId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JournalEntry", "Meal_MealId", "dbo.Meal");
            DropForeignKey("dbo.JournalEntry", "FoodItem_FoodId", "dbo.FoodItem");
            DropForeignKey("dbo.JournalEntry", "DayId", "dbo.Day");
            DropIndex("dbo.JournalEntry", new[] { "Meal_MealId" });
            DropIndex("dbo.JournalEntry", new[] { "FoodItem_FoodId" });
            DropIndex("dbo.JournalEntry", new[] { "DayId" });
            DropTable("dbo.JournalEntry");
            DropTable("dbo.Day");
        }
    }
}
