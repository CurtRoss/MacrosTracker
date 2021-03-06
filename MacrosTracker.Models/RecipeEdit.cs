using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Models
{
    public class RecipeEdit
    {
        [Required]
        public int RecipeId { get; set; }
      
        public string RecipeName { get; set; }
        public int HowManyPortionsDoesItMake { get; set; }
        public List<int> ListOfFoodIds { get; set; } = new List<int>();
    }
}
