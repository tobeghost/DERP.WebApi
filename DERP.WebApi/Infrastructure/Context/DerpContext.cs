﻿using DERP.WebApi.Domain.Entities;
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

    public IMongoCollection<Account> Account => _database.GetCollection<Account>(nameof(Account));
}