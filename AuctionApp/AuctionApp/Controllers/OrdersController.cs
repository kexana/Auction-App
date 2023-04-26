using AuctionApp.ModelDtos;
using AuctionApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.Controllers
{
    public class OrdersController : Controller
    {
        private IAuctionItemService auctionItemService;
        public OrdersController(IAuctionItemService auctionItemService) {
            this.auctionItemService = auctionItemService;
        }
        public IActionResult Index(string userId)
        {
            List<AuctionItemDto> Items = auctionItemService.GetAllAuctionItems().Where(x => x.buyerUserId == userId).ToList();
            return View(Items);
        }
    }
}
