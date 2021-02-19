using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Data
{
    public class FoodMeal
    {
        [Key]
        public int MealId { get; set; }
        public Meal Meal { get; set; }

        [Key]
        public int FoodId { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}
