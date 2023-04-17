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
    public class FeedbackController : BaseUserController
    {
        private readonly IAuctionFeedbackService auctionFeedbackService;
        private readonly IAuctionItemService auctionItemService;
        private readonly SignInManager<AuctionUser> signInManager;
        private readonly UserManager<AuctionUser> userManager;

        public FeedbackController(IAuctionItemService auctionItemService,IAuctionFeedbackService auctionFeedbackService, SignInManager<AuctionUser> signInManager, UserManager<AuctionUser> userManager)
        {
            this.auctionFeedbackService = auctionFeedbackService;
            this.auctionItemService = auctionItemService;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Create(string userId, long itemId)
        {
            if (signInManager.IsSignedIn(User))
            {
                AuctionUser sellerUser = await userManager.FindByIdAsync(userId);
                AuctionItemDto item = await auctionItemService.GetAuctionItemById(itemId);
                ViewBag.item = item;
                return View(sellerUser.ToDto());
            }
            return Redirect("/");
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(AuctionFeedbackDto feedbackDto, string userId, long itemId) {

            AuctionUser sellerUser = await userManager.FindByIdAsync(userId);
            AuctionItemDto item = await auctionItemService.GetAuctionItemById(itemId);
            await auctionFeedbackService.CreateAuctionFeedback(feedbackDto,sellerUser,item.ToEntity());

            return Redirect("/");
        }
    }
}
