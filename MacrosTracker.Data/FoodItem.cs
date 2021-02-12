using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Data
{
    public class FoodItem
    {
        [Key]
        public int FoodId { get; set; }


       

        [Required]
        public int MealId { get; set; }
        public string FoodName { get; set; }
        public decimal Amount { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbs { get; set; }
        public double Calories { get; set; }
        public int UserId { get; set; }
        public virtual List<FoodItem> ListOfFoodItems { get; set; } = new List<FoodItem>();

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
