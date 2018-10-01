using System;
using System.Collections.Generic;
using burgershack.Models;
using Microsoft.AspNetCore.Mvc;

namespace burgershack.Controllers
{
    [Route("api/[controller]")] //[controller] is string interpolation where [controller] equals the name of the file minus controller
    [ApiController]
    public class BurgersController : Controller //inside of Controller class is everything needed to handle CRUD
    {
        List<Burger> burgers;
        public BurgersController()
        {
            burgers = new List<Burger>();
            burgers.Add(new Burger("The Plain Jane", "Cheeseburger", 4.99m));
            burgers.Add(new Burger("Mushroom Swiss", "Swiss cheese and mushrooms", 8.99m));
            burgers.Add(new Burger("Baconater", "Double Cheeseburger with Bacon", 9.99m));
        }

        [HttpGet]
        public IEnumerable<Burger> Get()
        {
            return burgers;
        }

        [HttpPost]
        public Burger Post([FromBody] Burger burger) //dotnet can only take in at most two parameters => req.param and req.body
        {
            if (ModelState.IsValid)
            {
                burger = new Burger(burger.Name, burger.Description, burger.Price);
                //burger.Id = Guid.NewGuid(); because of new burger constructor id will not be forgotten and don't need to add id like this
                burgers.Add(burger);
                return burger;
            }
            throw new System.Exception("Invalid Burger");
        }
    }
}