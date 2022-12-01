using DERP.WebApi.Domain.Entities;
using MongoDB.Driver;

namespace DERP.WebApi.Infrastructure.Context;

public class DerpContext
{
    private readonly IMongoDatabase _database;
    
    public DerpContext(string connectionString)
    {
        var mongoUrl = new MongoUrl(connectionString);
        var mongoClient = new MongoClient(mongoUrl);
        _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
    }

    public IMongoDatabase Database => _database;

    public IMongoCollection<Customer> Customer => _database.GetCollection<Customer>(nameof(Customer));
    
    public IMongoCollection<Setting> Setting => _database.GetCollection<Setting>(nameof(Setting));

}