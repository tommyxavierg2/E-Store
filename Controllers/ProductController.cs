
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
        public IEnumerable<Product> GetAll(int state)
        {
            try
            {
                return dbContext.Products.Find(x => x.State == state).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
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

        /*[HttpGet("{userId}", Name = "GetProductByUserId")]
         public IEnumerable<Product> GetProductByUserId(string userId)
         {
             try 
             {
                var product =  dbContext.Products.Find(x => x.UserId == userId).ToList();
                return product;
             }
             catch (Exception ex)
             {
                 throw;
             }
         }*/

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
