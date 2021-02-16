using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Models
{
    public class FoodItemDetail
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
       // public virtual List<Meal> ListOfMeals { get; set; } = new List<Meal>();
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
