using System.Collections.Generic;
using burgershack.Models;
using Microsoft.AspNetCore.Mvc;

namespace burgershack.Controllers
{
    [Route("api/[controller]")] //[controller] is string interpolation where [controller] equals the name of the file minus controller
    [ApiController]
    public class SmoothiesController : Controller //inside of Controller class is everything needed to handle CRUD
    {
        List<Smoothie> smoothies;
        public SmoothiesController()
        {
            smoothies = new List<Smoothie>();
            smoothies.Add(new Smoothie("The Plain Jane", "Cheeseburger", 4.99m));
            smoothies.Add(new Smoothie("Mushroom Swiss", "Swiss cheese and mushrooms", 8.99m));
            smoothies.Add(new Smoothie("Baconater", "Double Cheeseburger with Bacon", 9.99m));
        }

        [HttpGet]
        public IEnumerable<Smoothie> Get()
        {
            return smoothies;
        }

        [HttpPost]
        public Smoothie Post([FromBody] Smoothie smoothie) //dotnet can only take in at most two parameters => req.param and req.body
        {
            smoothies.Add(smoothie);
            return smoothie;
        }
    }
}