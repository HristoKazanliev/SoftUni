﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    public abstract class Animal
    {
        private string name;
        private int age;
        private string gender;

        protected Animal(string name, int age, string gender)
        {
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }

        public string Name
        {
            get 
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(this.Name), "Invalid input"); 
                }
                this.name = value;
            }
        }

        public int Age
        {
            get 
            {
                return this.age;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentNullException(nameof(this.Age), "Invalid input");
                }
                this.age = value;
            }
        }

        public string Gender
        {
            get
            {
                return this.gender;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(this.Gender), "Invalid input");
                }
                this.gender = value;
            }
        }

        public abstract string ProduceSound();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name}")
              .AppendLine($"{this.Name} {this.Age} {this.Gender}")
              .AppendLine($"{this.ProduceSound()}");
            return sb.ToString().TrimEnd();
        }
    }
}
