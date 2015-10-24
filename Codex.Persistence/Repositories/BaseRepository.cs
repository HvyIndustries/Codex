namespace Codex.Persistence.Repositories
{
    using System.Configuration;
    using System.Linq;

    using Codex.Models.Models;

    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.Linq;

    public class BaseRepository<T> where T : BaseEntity
    {
        public BaseRepository()
        {
            var cs = ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString;
            
            var client = new MongoClient(cs);
            var server = client.GetServer();
            this.Database = server.GetDatabase(ConfigurationManager.AppSettings["MongoDatabase"]);

            // Use implied names; eg. "Users" for 'User' object
            this.Collection = this.Database.GetCollection<T>(nameof(T) + "s");
        }

        public MongoDatabase Database { get; set; }

        protected MongoCollection<T> Collection { get; set; } 

        public string Create(T entity)
        {
            this.Update(entity);
            return entity.Id;
        }

        public T GetById(string id)
        {
            var collection = this.Collection.AsQueryable();
            return collection.FirstOrDefault(p => p.Id == id);
        }

        public void Update(T entity)
        {
            this.Collection.Save(entity);
        }

        public void Delete(string id)
        {
            this.Collection.Remove(Query.EQ("_id", ObjectId.Parse(id)));
        }
    }
}
