
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
    public class ProductController : Controller
    {   
        private DbContext dbContext;

        public ProductController() 
        {
            dbContext = new DbContext();
        }

         [HttpPost]
        public string Create([FromBody] Product product)
        {
            try 
            {
                dbContext.Products.InsertOne(product);
                return "1 Record Added";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IEnumerable<Product> GetAll(string userId, int state, string name)
        {
            if (String.IsNullOrEmpty(userId) && String.IsNullOrEmpty(name)) {
                return dbContext.Products.Find(x => x.State == state).ToList();
            }
            else if (String.IsNullOrEmpty(name)) {
               return dbContext.Products.Find(x => x.UserId == userId && x.State == state).ToList(); 
            } 
            else {
               return dbContext.Products.Find(x => x.State == state && x.Name == name).ToList(); 
            }

        }

        [HttpGet("{id}", Name = "GetProductById")]
         public IEnumerable<Product> GetProductById(string id)
         {
             try 
             {
                return dbContext.Products.Find(x => x.Id == id).ToList();
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

        [HttpGet("{name}", Name = "GetProductByName")]
         public IEnumerable<Product> GetProductByName(string name)
         {
             try 
             {
                return dbContext.Products.Find(x => x.Name == name).ToList();
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

        [HttpPut("{id}")]
         public string Update(string id, [FromBody] Product product)
         {
             try
             {
                dbContext.Products.ReplaceOne(x => x.Id == id, product);
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
            var product = dbContext.Products.Find(t => t.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            dbContext.Products.DeleteOne(Builders<Product>.Filter.Eq("Id", id));
            return new NoContentResult();
         }


    }
}
