﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Models
{
    public class FoodItemEdit
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public decimal Amount { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbs { get; set; }
        public double Calories { get; set; }
    }
}