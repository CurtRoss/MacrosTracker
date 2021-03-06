using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MacrosTracker.Categories.MealCategory;

namespace MacrosTracker.Data
{
    public class Meal
    {
        [Key]
        public int MealId { get; set; }

        public int FoodId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Meal Name limited to 50 characters.")]
        public string MealName { get; set; }

        public List<int> ListOfFoodIds { get; set; } = new List<int>();

        public virtual List<FoodItem> ListOfFoods { get; set; } = new List<FoodItem>();
        
        public TypeofMealCategory Category { get; set; }

        public double Protein
        {
            get
            {
                var protein = ListOfFoods.Sum(e => e.Protein);
                return protein;
            }
        }

        public double Fat
        {
            get
            {
                var fat = ListOfFoods.Sum(e => e.Fat);
                return fat;
            }
        }

        public double Carbs
        {
            get
            {
                var carbs = ListOfFoods.Sum(e => e.Carbs);
                return carbs;
            }
        }

        public double Calories
        {
            get
            {
                var calories = ListOfFoods.Sum(e => e.Calories);
                return calories;
            }
        }


        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
