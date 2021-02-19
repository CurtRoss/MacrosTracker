using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Models
{
    public class MealEdit
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public string Category { get; set; }
        //public int Protein { get; set; }
        //public int Fat { get; set; }
        //public int Carbs { get; set; }
        //public int Calories { get; set; }

        //[Required]
        //public List<FoodItem> ListOfFoods { get; set; } = new List<FoodItem>();

    }
}
