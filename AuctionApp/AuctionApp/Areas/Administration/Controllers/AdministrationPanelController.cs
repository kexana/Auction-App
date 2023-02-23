using AuctionApp.WebModels.Administration.AdministrationPanel;
using AuctionApp.Data.Models;
using AuctionApp.WebModels.Administration.AdministrationPanel;
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
        private readonly AuctionAppDbContext dbContext;

        public AdministrationPanelController(IAuctionItemService auctionItemService, AuctionAppDbContext dbContext)
        {
            this.auctionItemService = auctionItemService;
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
        [HttpGet("Users")]
        public IActionResult UserIndex()
        {
            return View(new AdministrationPanelModel
            {
                Users = this.dbContext.Users.Select(user => user.ToDto()).ToList(),
            });
        }
    }
}
