using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace E_Store.Models
{
    public class DbContext
    {
        private readonly IMongoDatabase database;


        public DbContext()
        {
            database = new MongoClient("mongodb://localhost:27017").GetDatabase("db");
        }

        public IMongoCollection<User> Users
        {
             get 
             {
                 return database.GetCollection<User>("Users");
             } 
        }

        public IMongoCollection<Product> Products
        {
            get
            {
                return database.GetCollection<Product>("Products");
            }
        }

        
        public IMongoCollection<Cart> Carts
        {
            get
            {
                return database.GetCollection<Cart>("Carts");
            }
        }

        public IMongoCollection<Order> Orders
        {
            get
            {
                return database.GetCollection<Order>("Orders");
            }
        }

    }
}