using System.Collections.Generic;
using DERP.WebApi.Domain.Common;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace DERP.WebApi.Infrastructure.Extentions;

public static class MongoExtentions
{
    public static UpdateDefinition<T> UpdateBuilder<T>(this T obj) where T : MongoBaseEntity
    {
        var json = JsonConvert.SerializeObject(obj);
        var fiels = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        var builder = Builders<T>.Update;
        var update = builder.SetOnInsert(i => i.Id, obj.Id);
        foreach (var field in fiels)
        {
            update = update.Set(field.Key, field.Value);
        }
        return update;
    }
}