using System;
using System.Collections.Generic;
using burgershack.Models;
using burgershack.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace burgershack.Controllers
{
    [Route("api/[controller]")] //[controller] is string interpolation where [controller] equals the name of the file minus controller
    [ApiController]
    public class BurgersController : Controller //inside of Controller class is everything needed to handle CRUD
    {
        BurgersRepository _repo; //this gets instantiated in StartUp.cs
        public BurgersController(BurgersRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Burger> Get()
        {
            return _repo.GetAll();
        }
        [HttpGet("{id}")]
        public Burger Get([FromRoute] int id)
        {
            return _repo.GetById(id);
        }

        [HttpPost]
        public Burger Post([FromBody] Burger burger) //dotnet can only take in at most two parameters => req.param and req.body
        {
            if (ModelState.IsValid)
            {
                burger = new Burger(burger.Name, burger.Description, burger.Price);
                //burger.Id = Guid.NewGuid(); because of new burger constructor id will not be forgotten and don't need to add id like this
                return _repo.Create(burger); //can just return this because create returns a burger from the db
            }
            throw new System.Exception("Invalid Burger");
        }

        [HttpPut]
        public Burger Put([FromBody] Burger burger)
        {
            if (ModelState.IsValid)
            {
                return _repo.Update(burger);
            }
            throw new System.Exception("Must be a valid burger to change the burger.");
        }
    }
}