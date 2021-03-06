using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Models
{
    public class FoodItemCreate
    {
        [Required, MinLength(2, ErrorMessage = "Please enter at least two characters.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        
        public string FoodName { get; set; }
        public int FoodId { get; set; }
        [Required]
        public double Protein { get; set; }
        [Required]
        public double Fat { get; set; }
        [Required]
        public double Carbs { get; set; }
        public double Calories { get; set; }// zero references -- look at removing
    }
}
