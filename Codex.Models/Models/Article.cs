namespace Codex.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Article : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string AuthorId { get; set; }

        public DateTime CreationDateTime { get; set; }

        public DateTime EditDateTime { get; set; }

        [Required]
        public string Content { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
