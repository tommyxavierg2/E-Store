using System;
using System.Collections.Generic;
using System.Linq;
using E_Store.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace E_Store.Controllers
{
    [Produces("application/json")]        
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private DbContext dbContext;

        public UserController() 
        {
            dbContext = new DbContext();
        }

        [HttpPost]
        public string Create([FromBody] User user)
        {
            try 
            {
                dbContext.Users.InsertOne(user);
                return "1 Record Added";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            try
            {
                var users = dbContext.Users.Find(x => true).ToList();
                return users;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpGet("{id}", Name = "GetById")]
         public IEnumerable<User> GetById(string id)
         {
             try 
             {
                var user =  dbContext.Users.Find(x => x.Id == id).ToList();
                return user;
             }
             catch (Exception ex)
             {
                 throw;
             }
         }

        [HttpPut("{id}")]
         public string Update(string id, [FromBody] User user)
         {
             dbContext.Users.ReplaceOne(x => x.Id == id, user);
             return "Record Updated";
         }

         [HttpDelete("{id}")]
         public IActionResult Delete(string id)
         {
            var user = dbContext.Users.Find(t => t.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            dbContext.Users.DeleteOne(Builders<User>.Filter.Eq("Id", id));
            return new NoContentResult();
         }

    }

}