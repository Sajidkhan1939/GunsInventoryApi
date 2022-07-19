using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using InventoryApi.Model;

namespace InventoryApi
{
    public class DbContext
    {
        private readonly IMongoDatabase _database = null;
        public MongoClient DbClient =null;
        public DbContext(IOptions<Settings> settings)
        {
            MongoUrl uri = new MongoUrl(settings.Value.ConnectionString);
            DbClient = new MongoClient(uri);
            if (DbClient != null)
                _database = DbClient.GetDatabase(settings.Value.Database);
        }
        public IMongoCollection<User> User
        {
            get
            {
                try
                {
                    return _database.GetCollection<User>("User");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }
        public IMongoCollection<Guns> Gun
        {
            get
            {
                try
                {
                    return _database.GetCollection<Guns>("Gun");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }
    }
}
