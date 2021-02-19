namespace MacrosTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToMany : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meal",
                c => new
                    {
                        MealId = c.Int(nullable: false, identity: true),
                        FoodId = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        MealName = c.String(nullable: false, maxLength: 50),
                        Category = c.String(),
                        Protein = c.Int(nullable: false),
                        Fat = c.Int(nullable: false),
                        Carbs = c.Int(nullable: false),
                        Calories = c.Int(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        FoodItem_FoodId = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MealId)
                .ForeignKey("dbo.FoodItem", t => t.FoodItem_FoodId)
                .ForeignKey("dbo.FoodItem", t => t.FoodId, cascadeDelete: false)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.FoodId)
                .Index(t => t.FoodItem_FoodId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.FoodMeal",
                c => new
                    {
                        MealId = c.Int(nullable: false),
                        FoodId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MealId, t.FoodId })
                .ForeignKey("dbo.FoodItem", t => t.FoodId, cascadeDelete: false)
                .ForeignKey("dbo.Meal", t => t.MealId, cascadeDelete: false)
                .Index(t => t.MealId)
                .Index(t => t.FoodId);
            
            AddColumn("dbo.FoodItem", "MealId", c => c.Int(nullable: false));
            AddColumn("dbo.FoodItem", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.FoodItem", "Protein", c => c.Double(nullable: false));
            AddColumn("dbo.FoodItem", "Fat", c => c.Double(nullable: false));
            AddColumn("dbo.FoodItem", "Carbs", c => c.Double(nullable: false));
            AddColumn("dbo.FoodItem", "Calories", c => c.Double(nullable: false));
            AddColumn("dbo.FoodItem", "UserId", c => c.Guid(nullable: false));
            AddColumn("dbo.FoodItem", "CreatedUtc", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.FoodItem", "ModifiedUtc", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.FoodItem", "Meal_MealId", c => c.Int());
            AddColumn("dbo.FoodItem", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.FoodItem", "MealId");
            CreateIndex("dbo.FoodItem", "Meal_MealId");
            CreateIndex("dbo.FoodItem", "ApplicationUser_Id");
            AddForeignKey("dbo.FoodItem", "MealId", "dbo.Meal", "MealId", cascadeDelete: false);
            AddForeignKey("dbo.FoodItem", "Meal_MealId", "dbo.Meal", "MealId");
            AddForeignKey("dbo.FoodItem", "ApplicationUser_Id", "dbo.ApplicationUser", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meal", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.FoodItem", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.FoodMeal", "MealId", "dbo.Meal");
            DropForeignKey("dbo.FoodMeal", "FoodId", "dbo.FoodItem");
            DropForeignKey("dbo.FoodItem", "Meal_MealId", "dbo.Meal");
            DropForeignKey("dbo.Meal", "FoodId", "dbo.FoodItem");
            DropForeignKey("dbo.FoodItem", "MealId", "dbo.Meal");
            DropForeignKey("dbo.Meal", "FoodItem_FoodId", "dbo.FoodItem");
            DropIndex("dbo.FoodMeal", new[] { "FoodId" });
            DropIndex("dbo.FoodMeal", new[] { "MealId" });
            DropIndex("dbo.FoodItem", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.FoodItem", new[] { "Meal_MealId" });
            DropIndex("dbo.FoodItem", new[] { "MealId" });
            DropIndex("dbo.Meal", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Meal", new[] { "FoodItem_FoodId" });
            DropIndex("dbo.Meal", new[] { "FoodId" });
            DropColumn("dbo.FoodItem", "ApplicationUser_Id");
            DropColumn("dbo.FoodItem", "Meal_MealId");
            DropColumn("dbo.FoodItem", "ModifiedUtc");
            DropColumn("dbo.FoodItem", "CreatedUtc");
            DropColumn("dbo.FoodItem", "UserId");
            DropColumn("dbo.FoodItem", "Calories");
            DropColumn("dbo.FoodItem", "Carbs");
            DropColumn("dbo.FoodItem", "Fat");
            DropColumn("dbo.FoodItem", "Protein");
            DropColumn("dbo.FoodItem", "Amount");
            DropColumn("dbo.FoodItem", "MealId");
            DropTable("dbo.FoodMeal");
            DropTable("dbo.Meal");
        }
    }
}
