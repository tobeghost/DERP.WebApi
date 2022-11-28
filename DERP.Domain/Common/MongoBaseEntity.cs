using MongoDB.Bson;

namespace DERP.Domain.Common;

public abstract partial class MongoBaseEntity
{
    protected MongoBaseEntity()
    {
        _id = ObjectId.GenerateNewId().ToString();
    }
    
    private string _id;

    public string Id
    {
        get => _id;
        set
        {
            if (string.IsNullOrEmpty(value))
                _id = ObjectId.GenerateNewId().ToString();
            else
                _id = value;
        }
    }

    /// <summary>
    /// Gets or sets the date and time of instance creation
    /// </summary>
    public DateTime CreatedOnDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date and time of instance update
    /// </summary>
    public DateTime UpdatedOnDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the free data for entity
    /// </summary>
    public FreeValuationType FreeValuationData { get; set; } = new FreeValuationType();
}