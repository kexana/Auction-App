using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AuctionApp.Data.Models;

namespace AuctionApp.Areas.Users.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AuctionUser> _signInManager;

        public LogoutModel(SignInManager<AuctionUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await _signInManager.SignOutAsync();

            return LocalRedirect("/");
        }
    }
}
