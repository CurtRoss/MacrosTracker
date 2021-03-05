using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Data
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Recipe Name limited to 50 characters.")]
        public string RecipeName { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }

        public List<int> ListOfFoodIds { get; set; } = new List<int>();

        public virtual List<FoodItem> ListOfFoods { get; set; } = new List<FoodItem>();
        //public int HowManyServings { get; set; }

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
    }
}
