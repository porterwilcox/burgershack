using System.Threading.Tasks;
using burgershack.Models;
using burgershack.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace burgershack.Controllers
{
    [Route("[controller]")] //this is very specifically not an /api/ controller!
    public class AccountController : Controller
    {
        private readonly UserRepository _repo;

        [HttpPost("Login")]
        public async Task<User> Login([FromBody] UserLogin creds) //tasks are multithreaded processes that take in requests and divide the tasks amongst several threads. tasks are asynchronous
        {
            if (!ModelState.IsValid) { return null; }
            User user = _repo.Login(creds);
            if (user == null) { return null; }
            //if user was successfully returned from db then we must generate a cookie and give it to the client
            //token is generated from a claim
            user.SetClaims();
            await HttpContext.SignInAsync(user._principal);
            return user;
        }

        [HttpPost("Register")]
        public async Task<User> Register([FromBody] UserRegistration creds)
        {
            if (!ModelState.IsValid) { return null; }
            User user = _repo.Register(creds);
            if (user == null) { return null; }
            user.SetClaims();
            await HttpContext.SignInAsync(user._principal);
            return user;
        }

        [HttpDelete("Logout")]
        public async Task<bool> Logout()
        {
            await HttpContext.SignOutAsync();
            return true;
        }

        [Authorize] //this makes sure the user has logged in before can be authenticated
        [HttpGet("Authenticate")]
        public User Authenticate()
        {
            var id = HttpContext.User.Identity.Name;
            return _repo.GetUserById(id);
        }




        public AccountController(UserRepository repo)
        {
            _repo = repo;
        }
    }
}