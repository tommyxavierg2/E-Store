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

    }
}