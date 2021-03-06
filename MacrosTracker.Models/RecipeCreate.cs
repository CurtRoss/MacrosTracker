using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Models
{
    public class RecipeCreate
    {
        [Required]
        public string RecipeName { get; set; }
        [Required]
        public List<int> ListOfFoodIds { get; set; } = new List<int>();
        public DateTimeOffset CreatedUtc { get; set; }
        [Required]
        public int HowManyPortionsDoesRecipeMake { get; set; }
    }
}
