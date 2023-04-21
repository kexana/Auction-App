using Microsoft.AspNetCore.Mvc;
using AuctionApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using AuctionApp.Services;
using AuctionApp.ModelDtos;
using Microsoft.AspNetCore.Identity;
using AuctionApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using AuctionApp.Areas.Users.Pages.Account;
using System.Security.Claims;
using AuctionApp.Services.Mapping;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.Controllers
{
    public class ChatController : Controller
    {

        private readonly SignInManager<AuctionUser> signInManager;
        private readonly UserManager<AuctionUser> userManager;
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> PrivateMessaging()
        {
            List<AuctionUser> accounts = await userManager.Users.ToListAsync(); 
            return View(accounts);
        }
        public async Task<IActionResult> Message(string user)
        {
            List<AuctionUser> accounts = await userManager.Users.ToListAsync();
            KeyValuePair<List<AuctionUser>, string> model = new KeyValuePair<List<AuctionUser>, string>(accounts, user);

            return View("Message", model);
        }
    }
}
