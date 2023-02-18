using Microsoft.AspNetCore.Mvc;
using AuctionApp.Services;

namespace AuctionApp.Controllers
{
    public class AuctionItemController : Controller
    {
        private readonly IAuctionItemService auctionItemService;

        public AuctionItemController(IAuctionItemService auctionItemService)
        {
            this.auctionItemService = auctionItemService;
        }

        public IActionResult AuctionItemIndex()
        {
            return View(this.auctionItemService.GetAllForumSections());
        }
    }
}
