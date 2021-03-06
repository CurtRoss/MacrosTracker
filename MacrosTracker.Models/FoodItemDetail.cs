using MacrosTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Models
{
    public class FoodItemDetail
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbs { get; set; }
        public double Calories { get; set; }
        public virtual List<string> ListOfMealNames { get; set; } = new List<string>();

        [Display(Name = "Created Date")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified Date")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
