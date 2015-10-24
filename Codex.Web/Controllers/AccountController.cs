namespace Codex.Web.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;

    using Codex.Models.ViewModels;
    using Codex.Persistence.Repositories;
    using Codex.Security;

    public class AccountController : Controller
    {
        private readonly UsersRepository usersRepository;

        public AccountController()
        {
            this.usersRepository = new UsersRepository();
        }

        public ActionResult Login()
        {
            if (this.Request.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            this.ViewBag.AccountCreated = false;
            if ((string)this.TempData["FromRegisterPage"] == "true")
            {
                this.ViewBag.AccountCreated = "true";
            }

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel model, string returnUrl)
        {
            // TODO - prevent brute force attacks
            if (this.ModelState.IsValid)
            {
                var user = this.usersRepository.GetUserByEmail(model.Email);

                if (user != null)
                {
                    if (Crypto.ValidatePassword(model.PlainTextPassword, user.PasswordHash))
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, false);

                        // TODO - use returnUrl param here instead(?)
                        return this.RedirectToAction("Index", "Home");
                    }
                    
                    user.LoginAttempts++;
                    this.usersRepository.Update(user);
                }
                else
                {
                    this.ModelState.AddModelError("Email", "Your email or password was not correct");
                    this.ModelState.AddModelError("Password", "Your email or password was not correct");
                }
            }

            return this.View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return this.RedirectToAction("Index", "Home");
        }
    }
}
