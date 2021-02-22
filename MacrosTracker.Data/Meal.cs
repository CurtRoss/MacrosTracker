using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Data
{
    public class Meal
    {
        [Key]
        public int MealId { get; set; }

        //[ForeignKey(nameof(FoodItem))]
        //public int FoodId { get; set; }
        //public virtual FoodItem FoodItem { get; set; }

        [Required]
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Meal Name limited to 50 characters.")]
        public string MealName { get; set; }

        public List<int> ListOfFoodIds { get; set; }


        public virtual List<FoodItem> ListOfFoods { get; set; } = new List<FoodItem>();
        public string Category { get; set; }
        public double Protein
        {
            get
            {
                double totalProtein = 0;
                foreach(FoodItem foodItem in ListOfFoods)
                {
                    totalProtein = totalProtein + foodItem.Protein;
                }
                return totalProtein;
            }
        }
        public double Fat
        {
            get
            {
                double totalFat = 0;
                foreach (FoodItem foodItem in ListOfFoods)
                {
                    totalFat = totalFat + foodItem.Fat;
                }
                return totalFat;
            }
        }

        public double Carbs
        {
            get
            {
                double totalCarbs = 0;
                foreach (FoodItem foodItem in ListOfFoods)
                {
                    totalCarbs = totalCarbs + foodItem.Carbs;
                }
                return totalCarbs;
            }
        }
        
        public double Calories
        {
            get
            {
                double totalCalories = Carbs * 4 + Protein * 4 + Fat * 9;
                return totalCalories;
            }
        }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
