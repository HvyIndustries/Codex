namespace Codex.Models.Models
{
    using System.ComponentModel.DataAnnotations;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class User : BaseEntity
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public Role Role { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string SiteId { get; set; }

        // Used to prevent brute force attempts
        public int LoginAttempts { get; set; }
    }
}
