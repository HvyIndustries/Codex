namespace Codex.Web.Helpers
{
    using System.Web.Mvc;

    using Codex.Persistence.Repositories;
    using Codex.Security;

    public class DisplayHelpers
    {
        public static MvcHtmlString GetGravatarForUser(string email, int size = 50)
        {
            var emailHash = Md5.CreateHash(email);
            return new MvcHtmlString($"https://secure.gravatar.com/avatar/{emailHash}?s={size}");
        }

        //public static MvcHtmlString GetAvatarForUser(string email)
        //{
        //    var accountDb = new UsersRepository();
        //    var user = accountDb.GetUserByEmail(email);

        //    if (user.Avatar == Avatar.Gravatar)
        //    {
        //        return GetGravatarForUser(email);
        //    }

        //    return new MvcHtmlString("/Content/images/avatars/" + user.Avatar + ".png");
        //}
    }
}