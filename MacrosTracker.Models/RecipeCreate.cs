﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Models
{
    public class RecipeCreate
    {
        public string RecipeName { get; set; }
        public List<int> ListOfFoodIds { get; set; } = new List<int>();
        public DateTimeOffset CreatedUtc { get; set; }
    }
}