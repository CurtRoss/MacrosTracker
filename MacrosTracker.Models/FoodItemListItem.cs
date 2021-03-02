using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Models
{
    public class FoodItemListItem
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }

       
    }
}
