namespace Codex.Models.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the mongodb object id.
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
