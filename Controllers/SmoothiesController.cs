using System.Collections.Generic;
using burgershack.Models;
using burgershack.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace burgershack.Controllers
{
    [Route("api/[controller]")] //[controller] is string interpolation where [controller] equals the name of the file minus controller
    [ApiController]
    public class SmoothiesController : Controller //inside of Controller class is everything needed to handle CRUD
    {
        SmoothiesRepository _repo;
        public SmoothiesController(SmoothiesRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Smoothie> Get()
        {
            return _repo.GetAll();
        }

        [HttpPost]
        public Smoothie Post(Smoothie smoothie)
        {
            if (ModelState.IsValid)
            {
                return _repo.Create(smoothie);
            }
            throw new System.Exception("Invalid Smoothie");
        }
    }
}