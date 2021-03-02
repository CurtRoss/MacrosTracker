using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace MacrosTracker.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here

            return userIdentity;
        }
        [Required]
        public int Height { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public string MaleOrFemale { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public int Age
        {
            get
            {
                var age = DateTime.Now - DateOfBirth;
                var ageadj = age.Days / 365;
                return ageadj;
            }
        }
        public virtual List<Meal> ListOfMeals { get; set; } = new List<Meal>();
        public virtual List<FoodItem> ListOfFoods { get; set; } = new List<FoodItem>();

        public int DailyCalorieGoalToLoseWeight
        {
            get
            {
                if (MaleOrFemale.ToLower() == "male")
                {
                    var maleGoal = 10 * (Weight / 2.3) + (6.25 * (Height * 2.54)) - (5 * Age) + 5;
                    return (int)maleGoal;
                }
                var femaleGoal = (10 * (Weight / 2.3)) + (6.25 * (Height * 2.54)) - (5 * Age) - 161;
                return (int)femaleGoal;
            }
        }
        //public List<Recipe> MyRecipes { get; set; }
        //public List<Food> UsersFoods { get; set; }
        //public List<DailyMeal> UsersMeals { get; set; }



    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }




        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Meal> DailyMeals { get; set; }
        public DbSet <FoodMeal> FoodMeals { get; set; }
        public DbSet <JournalEntry> JournalEntries { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Conventions
                .Remove<PluralizingTableNameConvention>();

            modelBuilder
                .Configurations
                .Add(new IdentityUserLoginConfiguration())
                .Add(new IdentityUserRoleConfiguration());

            //modelBuilder.Entity<FoodItem>()
            //    .HasMany<Meal>(f => f.ListOfMeals)
            //    .WithMany(m => m.ListOfFoods);


            //modelBuilder
            //    .Entity<FoodMeal>()
            //    .HasKey(fm => new { fm.FoodId, fm.MealId });

            //modelBuilder
            //    .Entity<FoodMeal>()
            //    .HasOne<FoodItem>(fm => fm.FoodItem)
            //    .WithMany(f => f.ListOfMeals)
            //    .HasForeignKey(fm => fm.FoodId);

            //modelBuilder.Entity<FoodMeal>()
            //    .HasOne<Meal>(fm => fm.Meal)
            //    .WithMany(m => m.ListOfFoods)
            //    .HasForeignKey(fm => fm.MealId);

        }

        //protected override void OnModelCreating2(ModelBuilder modelBuilder)

        

        public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
        {
            public IdentityUserLoginConfiguration()
            {
                HasKey(iul => iul.UserId);
            }
        }
        public class IdentityUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
        {
            public IdentityUserRoleConfiguration()
            {
                HasKey(iur => iur.UserId);
            }
        }
    }
}