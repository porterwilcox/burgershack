using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace burgershack.Models
{
    public class UserLogin //this is a helper model
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
    public class UserRegistration //this is a helper model
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public bool Active { get; set; } = true;

        public string Username { get; set; }

        [Required]
        internal string Hash { get; set; } //internal accessor: any file within the project can retreive this property but it will never be sent to the fron end

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public ClaimsPrincipal _principal { get; private set; } //this is essentially the token

        internal void SetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, Email), //this is saying req.session.uemail = email
                new Claim(ClaimTypes.Name, Id) //can't use ClaimTypes.Id because they don't have that feature. this is saying req.session.uid = id
            };
            var userIdentity = new ClaimsIdentity(claims, "login");
            _principal = new ClaimsPrincipal(userIdentity);
        }
    }
}