using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;
using AuctionApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuctionApp.Areas.Users.Pages
{
    public class ChatSelectPageModel : PageModel
    {
        private readonly IAuctionPrivateMessageService auctionPMService;
        private readonly SignInManager<AuctionUser> signInManager;
        private readonly UserManager<AuctionUser> userManager;

        public IQueryable<AuctionUserDto> ChatWithUsers { get; private set; }

        public ChatSelectPageModel(IAuctionPrivateMessageService auctionPMService, SignInManager<AuctionUser> signInManager, UserManager<AuctionUser> userManager)
        {
            this.auctionPMService = auctionPMService;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            AuctionUser senderUser = await userManager.FindByIdAsync(userId);

            if (senderUser == null)
            {
                return NotFound();
            }

            IQueryable<AuctionUserDto> users = auctionPMService.GetAllAuctionUserWithCorrespondance(userId);

            // Build account view model
            this.ChatWithUsers = users;

            return Page();
        }
    }
}
