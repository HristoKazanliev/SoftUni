﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShortage
{
    class Rebel : IBuyer
    {
        private const int DefaultFoodPortion = 5;

        public Rebel(string name, int age, string group)
        {
            this.Name = name;
            this.Age = age;
            this.Group = group;
            this.Food = 0;
        }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Group { get; set; }

        public int Food { get; set; }

        public void BuyFood()
        {
            this.Food += DefaultFoodPortion;
        }
    }
}
