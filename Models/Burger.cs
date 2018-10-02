using System;
using System.ComponentModel.DataAnnotations;

namespace burgershack.Models
{
    public class Burger
    {
        public int Id { get; set; }

        [Required]
        [MinLength(6)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Burger(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        // public Burger(Burger burger)
        // {
        //     Id = Guid.NewGuid();
        //     Name = burger.Name;
        //     Description = burger.Description;
        //     if (burger.Price > 5)
        //     {
        //         Price = burger.Price;
        //     }
        //     else
        //     {
        //         Price = 5;
        //     }
        // }        
    }
}