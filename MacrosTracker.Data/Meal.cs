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

        
        public virtual List<FoodItem> ListOfFoods { get; set; } = new List<FoodItem>();
        public string Category { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set; }
        public int Calories { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
