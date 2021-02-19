using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Models
{
    public class MealCreate
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Meal Name limited to 50 characters.")]
        public string MealName { get; set; }
        //public List<FoodItem> ListOfFoods { get; set; } = new List<FoodItem>();
        public string Category { get; set; }
    }
}
