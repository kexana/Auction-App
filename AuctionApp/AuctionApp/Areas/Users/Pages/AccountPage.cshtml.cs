using AuctionApp.Services;
using AuctionApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AuctionApp.ModelDtos;

namespace AuctionApp.Areas.Users.Pages
{
    public class AccountPageModel : PageModel
    {
        private readonly UserManager<AuctionUser> _userManager;
        private readonly IAuctionItemService _auctionItemService;
        public AuctionUser AuctionUser {get; set;}
        public IQueryable<AuctionItemDto> AuctionItems { get; set;}

        public AccountPageModel(UserManager<AuctionUser> userManager, IAuctionItemService auctionItemService)
        {
            _userManager = userManager;
            _auctionItemService = auctionItemService;
        }

        public AccountPageModel Account { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Get current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Get auction items created by current user
            IQueryable<AuctionItemDto> auctionItems = _auctionItemService.GetAllAuctionItemsByUserId(user.Id);

            // Build account view model
            this.AuctionUser = user;
            this.AuctionItems = auctionItems;

            return Page();
        }
    }
}
