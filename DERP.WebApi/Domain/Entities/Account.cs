using DERP.WebApi.Domain.Common;

namespace DERP.WebApi.Domain.Entities;

public class Account : MongoBaseEntity
{
    public string Name { get; set; }
}