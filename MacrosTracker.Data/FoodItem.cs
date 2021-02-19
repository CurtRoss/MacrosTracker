using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Data
{
    public class FoodItem
    {
        public string FoodName { get; set; }
        [Key]
        public int FoodId { get; set; }
    }
}
