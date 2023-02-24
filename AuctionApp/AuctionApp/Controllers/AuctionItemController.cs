using Microsoft.AspNetCore.Mvc;
using AuctionApp.Services;
using AuctionApp.ModelDtos;
using Microsoft.AspNetCore.Identity;
using AuctionApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using AuctionApp.Areas.Users.Pages.Account;
using System.Security.Claims;
using AuctionApp.Services.Mapping;

namespace AuctionApp.Controllers
{
    public class AuctionItemController : BaseUserController
    {
        private readonly IAuctionItemService auctionItemService;
        private readonly SignInManager<AuctionUser> signInManager;
        private readonly UserManager<AuctionUser> userManager;

        public AuctionItemController(IAuctionItemService auctionItemService, SignInManager<AuctionUser> signInManager, UserManager<AuctionUser> userManager)
        {
            this.auctionItemService = auctionItemService;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult AuctionItemIndex()
        {
            return View(this.auctionItemService.GetAllAuctionItems());
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Create()
        {
            if (signInManager.IsSignedIn(User))
            {
                return View();
            }
            return Redirect("/");
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(AuctionItemDto auuctionItemDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AuctionUser auctionUser = await userManager.FindByIdAsync(userId);
            await auctionItemService.CreateAuctionItem(auuctionItemDto, auctionUser);

            return Redirect("/");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            AuctionItemDto auctionItemDto = await auctionItemService.GetAuctionItemById(id);
            return View("/AdministrationPanelController/Edit",auctionItemDto);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(long id, AuctionItemDto auuctionItemDto)
        {
            await auctionItemService.UpdateAuctionItem(id,auuctionItemDto);

            return Redirect("/");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            AuctionItemDto auctionItemDto = await auctionItemService.GetAuctionItemById(id);
            return View("/AdministrationPanelController/Delete", auctionItemDto);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(long id, AuctionItemDto auuctionItemDto)
        {
            await auctionItemService.DeleteAuctionItem(id);

            return Redirect("/");
        }
        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            AuctionItemDto auctionItemDto = await auctionItemService.GetAuctionItemById(id);
            return View("/AdministrationPanelController/Details", auctionItemDto);
        }
        [HttpGet]
        public async Task<IActionResult> AuctionPage(long itemId) 
        {
            AuctionItemDto auctionItemDto = await auctionItemService.GetAuctionItemById(itemId);
            return View(auctionItemDto);
        }
    }
}
