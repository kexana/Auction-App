using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;
using AuctionApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace AuctionApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuctionItemService _auctionItemService;

        public HomeController(ILogger<HomeController> logger, IAuctionItemService auctionItemService)
        {
            _logger = logger;
            _auctionItemService= auctionItemService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<AuctionItemDto> trendingItems = new List<AuctionItemDto>();
            trendingItems = _auctionItemService.GetFilteredAuctionItems();
            return View(trendingItems);
        }
        [HttpPost]
        public IActionResult Index(string? search)
        {
            IEnumerable<AuctionItemDto> trendingItems = new List<AuctionItemDto>();
            trendingItems = _auctionItemService.GetAuctionItemsByName(search);
            return View(trendingItems);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}