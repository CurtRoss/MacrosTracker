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
        [Key, Column(Order = 0)]
        [ForeignKey(nameof(Meal))]
        public int MealId { get; set; }
        public Meal Meal { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey(nameof(FoodItem))]
        public int FoodId { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}
