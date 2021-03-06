using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Models
{
    public class FoodItemEdit
    {
        [Required]
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbs { get; set; }
        public double Calories  //there are zero references -- may be able to remove this
        {
            get
            {
                double totalCalories = Carbs * 4 + Protein * 4 + Fat * 9;
                return totalCalories;
            }
        }
    }
}
