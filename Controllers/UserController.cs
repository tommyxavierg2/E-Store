using System;
using System.Collections.Generic;
using System.Linq;
using E_Store.Models;
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
        public IEnumerable<User> Create([FromBody] User user)
        {
            try 
            {
                dbContext.Users.InsertOne(user);
                return dbContext.Users.Find(x => x.Email == user.Email).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IEnumerable<User> GetAllOrGetUserByEmailAndPassword(string email, string password)
        {
            if (String.IsNullOrEmpty(email) && String.IsNullOrEmpty(password)) {
                return dbContext.Users.Find(x => true).ToList();;
            } 
            else {
                return dbContext.Users.Find(x => x.Email == email && x.Password == password).ToList();
            }

        }

        [HttpGet("{id}", Name = "GetUserById")]
         public IEnumerable<User> GetUserById(string id)
         {
            return dbContext.Users.Find(x => x.Id == id).ToList();
         }

       /* [HttpGet("{email, password}", Name = "GetUserByEmailAndPassword")]
         public IEnumerable<User> GetUserByEmailAndPassword()
         {
            var users = dbContext.Users; 
            return users.Find(x => x.Email == email && x.Password == password).ToList();
         } /* */

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