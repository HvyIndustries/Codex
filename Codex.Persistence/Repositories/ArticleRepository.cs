namespace Codex.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Codex.Models.Models;

    using MongoDB.Driver.Linq;

    public class ArticleRepository : BaseRepository<Article>
    {
        public IEnumerable<Article> GetArticlesCreatedByUser(string userId)
        {
            var articles = this.Collection.AsQueryable();
            return articles.Where(p => p.AuthorId == userId);
        }
    }
}
