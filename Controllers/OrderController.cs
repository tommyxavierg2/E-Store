
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
    public class OrderController : Controller
    {   
        private DbContext dbContext;

        public OrderController() 
        {
            dbContext = new DbContext();
        }

         [HttpPost]
        public string Create([FromBody] Order order)
        {
            try 
            {
                dbContext.Orders.InsertOne(order);
                return "1 Record Added";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IEnumerable<Order> GetAll(string company)
        {
            return dbContext.Orders.Find(x => x.Company == company).ToList();
        }

        [HttpPut("{id}")]
         public string Update(string id, [FromBody] Order order)
         {
             try
             {
                dbContext.Orders.ReplaceOne(x => x.Id == id, order);
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
            var cart = dbContext.Orders.Find(t => t.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            dbContext.Orders.DeleteOne(Builders<Order>.Filter.Eq("Id", id));
            return new NoContentResult();
         }


    }
}
