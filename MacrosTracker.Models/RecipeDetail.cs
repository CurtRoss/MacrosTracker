using MacrosTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Models
{
   public class RecipeDetail
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbs { get; set; }
        public double Calories { get; set; }
        public List<string> ListOfFoodNames { get; set; } = new List<string>();

        [Display(Name = "Created Date")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified Date")]
        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
