namespace Codex.Persistence.Repositories
{
    using System.Linq;

    using Codex.Models.Models;

    using MongoDB.Driver.Linq;

    public class UsersRepository : BaseRepository<User>
    {
        public User GetUserByEmail(string email)
        {
            var users = this.Collection.AsQueryable();
            return users.FirstOrDefault(p => p.Email == email);
        }

        public string GetPasswordHashForUser(string email)
        {
            var user = this.GetUserByEmail(email);
            return user?.PasswordHash;
        }
    }
}
