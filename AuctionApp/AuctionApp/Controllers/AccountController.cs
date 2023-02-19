using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AuctionApp.Data.Models;
using AuctionApp.Areas.Users.Pages.Account;

namespace AuctionApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AuctionUser> _userManager;
        private readonly SignInManager<AuctionUser> _signInManager;

        public AccountController(UserManager<AuctionUser> userManager, SignInManager<AuctionUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Identity/Login.cshtml");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Areas/Identity/Pages/Account/Register.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            //if (ModelState.IsValid)
            //{
                AuctionUser user = new AuctionUser { UserName = model.Input.Email, Email = model.Input.Email };
                var result = await _userManager.CreateAsync(user, model.Input.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Login));
                }
            //    foreach (var error in result.Errors)
            //    {
            //        ModelState.AddModelError(string.Empty, error.Description);
            //    }
            //}
            return View();
        }
    }
}