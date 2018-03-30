
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using E_Store.Models;

namespace E_Store.Controllers
{
    [Produces("application/json")]        
    [Route("api/[controller]")]
    public class CartController : Controller
    {   
        private DbContext dbContext;

        public CartController() 
        {
            dbContext = new DbContext();
        }

         [HttpPost]
        public string Create([FromBody] Cart cart)
        {
            try 
            {
                dbContext.Carts.InsertOne(cart);
                return "1 Record Added";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IEnumerable<Cart> GetAll(string userId, int state)
        {
            if (String.IsNullOrEmpty(userId)) {
                return dbContext.Carts.Find(x => x.State == state).ToList();
            }
            else {
                return dbContext.Carts.Find(x => x.UserId == userId && x.State == state).ToList();
            }
        }
        
        /*
        [HttpGet("{userId}", Name = "GetCartByUserId")]
         public IEnumerable<Cart> GetCartByUserId(string userId, int state)
         {
             try 
             {
                return dbContext.Carts.Find(x => x.UserId == userId && x.State == state).ToList();
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }*/

        [HttpPut("{id}")]
         public string Update(string id, [FromBody] Cart cart)
         {
             try
             {
                dbContext.Carts.ReplaceOne(x => x.Id == id, cart);
                return "Record Updated";
             }
             catch (Exception ex)
             {
                 throw ex;
             }

         }

         [HttpDelete("{id}")]
         public IActionResult Delete(string id)
         {
            var cart = dbContext.Carts.Find(t => t.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            dbContext.Carts.DeleteOne(Builders<Cart>.Filter.Eq("Id", id));
            return new NoContentResult();
         }


    }
}
