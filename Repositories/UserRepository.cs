using System;
using System.Data;
using System.Linq;
using BCrypt.Net;
using burgershack.Models;
using Dapper;

namespace burgershack.Repositories
{
    public class UserRepository
    {
        IDbConnection _db;
        //REGISTER
        public User Register(UserRegistration creds)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(creds.Password);
            var id = Guid.NewGuid().ToString();
            int success = _db.Execute(@"INSERT INTO users (id, username, email, hash)
            VALUES (@id, @username, @email, @hash);",
            new
            {
                id,
                username = creds.Username,
                email = creds.Email,
                hash
            });
            if (success == 1)
            {
                return new User()
                {
                    Username = creds.Username,
                    Email = creds.Email,
                    Hash = null,
                    Id = id
                };
            }
            return null;
        }
        public User Login(UserLogin creds)
        {
            User user = _db.Query<User>("SELECT * FROM users WHERE email = @Email", creds).FirstOrDefault();
            if (user == null) { return null; }
            bool validPassword = BCrypt.Net.BCrypt.Verify(creds.Password, user.Hash);
            if (!validPassword) { return null; }
            user.Hash = null;
            return user;
        }
        //ctor
        public UserRepository(IDbConnection db)
        {
            _db = db;
        }

        internal User GetUserById(string id)
        {
            User user = _db.Query<User>("SELECT * FROM users WHERE id = @id", new { id }).FirstOrDefault();
            if (user == null) { return null; }
            user.Hash = null;
            return user;
        }
    }
}