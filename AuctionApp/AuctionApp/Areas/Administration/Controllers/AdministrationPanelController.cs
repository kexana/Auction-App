using AuctionApp.WebModels.Administration.AdministrationPanel;
using AuctionApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using AuctionApp.Services;
using Microsoft.AspNetCore.Identity;
using AuctionApp.Data;
using AuctionApp.Services.Mapping;

namespace AuctionApp.Areas.Administration.Controllers
{
    [Route("/Administration/Home")]
    public class AdministrationPanelController : BaseAdministrationController
    {
        private readonly IAuctionItemService auctionItemService;
        private readonly IAuctionBidService auctionBidService;
        private readonly IAuctionFeedbackService auctionFeedbackService;
        private readonly AuctionAppDbContext dbContext;

        public AdministrationPanelController(IAuctionItemService auctionItemService, IAuctionBidService auctionBidService, IAuctionFeedbackService auctionFeedbackService, AuctionAppDbContext dbContext)
        {
            this.auctionItemService = auctionItemService;
            this.auctionBidService= auctionBidService;
            this.auctionFeedbackService= auctionFeedbackService;
            this.dbContext = dbContext;
        }
        [HttpGet("ItemsIndex")]
        public IActionResult AuctionItemIndex()
        {
            return View(new AdministrationPanelModel
            {
                Items = this.auctionItemService.GetAllAuctionItems(true).ToList(),
            });
        }
        [HttpGet("BidsIndex")]
        public IActionResult AuctionBidIndex()
        {
            return View(new AdministrationPanelModel
            {
                Bids = this.auctionBidService.GetAllAuctionBids(true).ToList(),
            });
        }
        [HttpGet("FeedbackIndex")]
        public IActionResult AuctionFeedbackIndex()
        {
            return View(new AdministrationPanelModel
            {
                Feedback = this.auctionFeedbackService.GetAllAuctionFeedback(true).ToList(),
            });
        }
        [HttpGet("Users")]
        public IActionResult UserIndex()
        {
            return View(new AdministrationPanelModel
            {
                Users = this.dbContext.Users.Select(user => user.ToDto(true,true)).ToList(),
            });
        }
    }
}
