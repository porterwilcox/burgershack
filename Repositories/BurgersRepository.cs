using System.Collections.Generic;
using System.Data;
using System.Linq;
using burgershack.Models;
using Dapper;

namespace burgershack.Repositories
{
    public class BurgersRepository
    {
        private IDbConnection _db;
        private string tableName = "burgers"; //this is ok to trust myself but do not ever trust information supplied by users
        public BurgersRepository(IDbConnection db)
        {
            _db = db;
        }

        //
        //CRUD VIA SQL
        //

        //Get all burgers
        public IEnumerable<Burger> GetAll()
        {
            return _db.Query<Burger>($"SELECT * FROM {tableName};");
        }

        //Get burger by id
        public Burger GetById(int id)
        {
            //ok to trust variable id because dapper is checking for us that it's safe by removing SQL commands
            //in SQL 'new {}' creates new element of type dynamic 
            //* below is targeting every column of found object i.e., burger - if choose just one field then query<T> must match T of column
            return _db.Query<Burger>("SELECT * FROM burgers WHERE id = @id;", new { id }).FirstOrDefault(); //FirstOrDefault is cool method that checks ienumerable length before targeting [0] to avoid error
        }

        //Get burgers by user id
        public IEnumerable<Burger> GetBurgersByUserId(string id)
        {
            return _db.Query<Burger>(@"SELECT * FROM userburgers
            INNER JOIN burgers ON burgers.id = userburgers.burgerId
            WHERE userId = @id;", new { id });
        }

        //Create burger
        public Burger Create(Burger burger)
        {
            //executescaler has permission to write and also can run multiple lines of SQL where the next command doesn't run until the current command finishes
            //don't use string interpolation with this method
            int id = _db.ExecuteScalar<int>(@"
                INSERT INTO burgers (name, description, price)
                VALUES (@Name, @Description, @Price);
                SELECT LAST_INSERT_ID();", burger);
            //we are missing safety checks here
            burger.Id = id;
            return burger;
        }
        //Update burger
        // public Burger Update(Burger burger)
        // {
        //     //DO NOT FORGET TO SPECIFY WHICH BURGER
        //     _db.Execute(@"UPDATE burgers SET (name, description, price)
        //     VALUES (@Name, @Description, @Price)
        //     WHERE id = @Id;", burger);
        //     return burger;
        // }

        public Burger Update(Burger burger)
        {
            //DO NOT FORGET TO SPECIFY WHICH BURGER
            _db.Execute(@"UPDATE burgers 
            SET name = @Name, description = @Description, price = @Price
            WHERE id = @Id;", burger);
            return burger;
        }

        //Delete burger
        //this is okay to have multiple methods with different signatures because of c# overloading
        public Burger Delete(Burger burger)
        {
            _db.Execute(@"DELETE FROM burgers WHERE id = @Id", burger);
            return burger;
        }
        //or do this
        public int Delete(int id)
        {
            return _db.Execute(@"DELETE FROM burgers WHERE id = @Id", new { id });
        }
    }
}