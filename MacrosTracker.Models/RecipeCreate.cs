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
        public string RecipeName { get; set; }
        public List<int> ListOfFoodIds { get; set; } = new List<int>();

        [Display(Name = "Created Date")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
