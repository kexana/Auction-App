using Microsoft.AspNetCore.Mvc;
using AuctionApp.Services;
using AuctionApp.ModelDtos;
using Microsoft.AspNetCore.Identity;

namespace AuctionApp.Controllers
{
    public class AuctionItemController : Controller
    {
        private readonly IAuctionItemService auctionItemService;

        public AuctionItemController(IAuctionItemService auctionItemService)
        {
            this.auctionItemService = auctionItemService;
        }
        [HttpGet]
        public IActionResult AuctionItemIndex()
        {
            return View(this.auctionItemService.GetAllAuctionItems());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AuctionItemDto auuctionItemDto)
        {
            auctionItemService.CreateAuctionItem(auuctionItemDto);

            return View("~/Views/Home/Index.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            AuctionItemDto auctionItemDto = await auctionItemService.GetAuctionItemById(id);
            return View(auctionItemDto);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(long id, AuctionItemDto auuctionItemDto)
        {
            await auctionItemService.UpdateAuctionItem(id,auuctionItemDto);

            return View("~/Views/Home/Index.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            AuctionItemDto auctionItemDto = await auctionItemService.GetAuctionItemById(id);
            return View(auctionItemDto);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(long id, AuctionItemDto auuctionItemDto)
        {
            await auctionItemService.DeleteAuctionItem(id);

            return View("~/Views/Home/Index.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            AuctionItemDto auctionItemDto = await auctionItemService.GetAuctionItemById(id);
            return View(auctionItemDto);
        }
    }
}
