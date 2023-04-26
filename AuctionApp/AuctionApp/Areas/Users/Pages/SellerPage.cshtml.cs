using AuctionApp.Services;
using AuctionApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AuctionApp.ModelDtos;

namespace AuctionApp.Areas.Users.Pages
{
    public class SellerPageModel : PageModel
    {
        private readonly UserManager<AuctionUser> _userManager;
        private readonly IAuctionItemService _auctionItemService;
        private readonly IAuctionFeedbackService _auctionFeedbackService;
        public AuctionUser AuctionUser {get; set;}
        public IQueryable<AuctionItemDto> AuctionItems { get; set;}
        public IQueryable<AuctionFeedbackDto> AuctionFeedback { get; set;}

        public decimal starRating { get; set;}

        public SellerPageModel(UserManager<AuctionUser> userManager, IAuctionFeedbackService auctionFeedbackService, IAuctionItemService auctionItemService)
        {
            _userManager = userManager;
            _auctionItemService = auctionItemService;
            _auctionFeedbackService = auctionFeedbackService;
        }

        public AccountPageModel Account { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            // Get current user
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Get auction items created by current user
            IQueryable<AuctionItemDto> auctionItems = await _auctionItemService.GetAllAuctionItemsByUserId(user.Id);
            IQueryable<AuctionFeedbackDto> feedback = _auctionFeedbackService.GetAllAuctionFeedbackBySellerId(user.Id);
            starRating = _auctionFeedbackService.CalculateRatingForSeller(user.Id);

            // Build account view model
            this.AuctionUser = user;
            this.AuctionItems = auctionItems;
            this.AuctionFeedback = feedback;

            return Page();
        }
    }
}
