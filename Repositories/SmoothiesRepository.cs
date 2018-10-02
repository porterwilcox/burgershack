using System.Collections.Generic;
using System.Data;
using System.Linq;
using burgershack.Controllers;
using burgershack.Models;
using Dapper;

namespace burgershack.Repositories
{
    public class SmoothiesRepository
    {
        private IDbConnection _db;
        private string tableName = "smoothies"; //this is ok to trust myself but do not ever trust information supplied by users
        public SmoothiesRepository(IDbConnection db)
        {
            _db = db;
        }

        //
        //CRUD VIA SQL
        //

        //Get all smoothies
        public IEnumerable<Smoothie> GetAll()
        {
            return _db.Query<Smoothie>($"SELECT * FROM {tableName};");
        }

        //Get smoothie by id
        public Smoothie GetById(int id)
        {
            return _db.Query<Smoothie>($"SELECT * FROM {tableName} WHERE id = @id;", new { id }).FirstOrDefault();
        }

        //Create Smoothie
        public Smoothie Create(Smoothie smoothie)
        {
            int id = _db.ExecuteScalar<int>(@"
                INSERT INTO smoothies (name, description, price)
                VALUES (@Name, @Description, @Price);
                SELECT LAST_INSERT_ID();", smoothie);
            smoothie.Id = id;
            return smoothie;
        }

        //Update Smoothie
        public Smoothie Update(Smoothie smoothie)
        {
            _db.Execute(@"UPDATE smoothies SET (name, description, price)
            VALUES (@Name, @Description, @Price)
            WHERE id = @Id;", smoothie);
            return smoothie;
        }

        //Delete smoothie
        public Smoothie Delete(Smoothie smoothie)
        {
            _db.Execute(@"DELETE FROM smoothies
            WHERE id = @Id", smoothie);
            return smoothie;
        }
        //or do this
        public int Delete(int id)
        {
            return _db.Execute("DELETE FROM smoothies WHERE id = @id", new { id });
        }
    }
}