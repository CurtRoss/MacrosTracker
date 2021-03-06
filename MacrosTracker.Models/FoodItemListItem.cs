using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Models
{
    public class FoodItemListItem
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }

        [Display(Name = "Created Date")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified Date")]
        public DateTimeOffset? ModifiedUtc { get; set; }       
    }
}
