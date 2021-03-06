using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Models
{
    public class RecipeListItem
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }

        [Display(Name = "Created Date")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
