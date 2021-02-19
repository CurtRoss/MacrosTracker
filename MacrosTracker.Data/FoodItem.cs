﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Data
{
    public class FoodItem
    {

        [Key]
        public int FoodId { get; set; }

        [ForeignKey(nameof(Meal))]
        public int MealId { get; set; }
        public virtual Meal Meal { get; set; }


        public string FoodName { get; set; }
        public decimal Amount { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbs { get; set; }
        public double Calories { get; set; }
        public Guid UserId { get; set; }
        public virtual List<Meal> ListOfMeals { get; set; } = new List<Meal>();

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }

    }
}
