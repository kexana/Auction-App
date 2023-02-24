using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;
using AuctionApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuctionApp.Areas.Administration.Controllers
{
    [Route("/Administration/AuctionItems")]
    public class AuctionItemAdministrationController : BaseAdministrationController
    {
        private readonly IAuctionItemService auctionItemService;
        private readonly SignInManager<AuctionUser> signInManager;
        private readonly UserManager<AuctionUser> userManager;

        public AuctionItemAdministrationController(IAuctionItemService auctionItemService, SignInManager<AuctionUser> signInManager, UserManager<AuctionUser> userManager)
        {
            this.auctionItemService = auctionItemService;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(long id)
        {
            AuctionItemDto auctionItemDto = await auctionItemService.GetAuctionItemById(id);
            return View(auctionItemDto);
        }
        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(long id, AuctionItemDto auuctionItemDto)
        {
            await auctionItemService.UpdateAuctionItem(id, auuctionItemDto);

            return Redirect("/Administration/Home/ItemsIndex");
        }
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            AuctionItemDto auctionItemDto = await auctionItemService.GetAuctionItemById(id);
            return View(auctionItemDto);
        }
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(long id, AuctionItemDto auuctionItemDto)
        {
            await auctionItemService.DeleteAuctionItem(id);

            return Redirect("/Administration/Home/ItemsIndex");
        }
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(long id)
        {
            AuctionItemDto auctionItemDto = await auctionItemService.GetAuctionItemById(id);
            return View(auctionItemDto);
        }
    }
}
