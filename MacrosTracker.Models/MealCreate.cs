using MacrosTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MacrosTracker.Categories.MealCategory;

namespace MacrosTracker.Models
{
    public class MealCreate
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Meal Name limited to 50 characters.")]
        public string MealName { get; set; }

        [Required]
        public List<int> ListOfFoodIds { get; set; }
        public List<FoodItem> ListOfFoods { get; set; } 
        public TypeofMealCategory Category { get; set; }
    }
}
