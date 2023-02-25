using Microsoft.AspNetCore.Mvc;
using AuctionApp.Services;
using AuctionApp.ModelDtos;
using Microsoft.AspNetCore.Identity;
using AuctionApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using AuctionApp.Areas.Users.Pages.Account;
using System.Security.Claims;
using AuctionApp.Services.Mapping;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Cryptography;

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
                if (TempData.ContainsKey("Error") && TempData["Error"]!=null)
                {
                    foreach (var error in (IEnumerable<string>)TempData["Error"])
                    {
                        ModelState.AddModelError("create", error.ToString());
                    }
                }
                return View();
            }
            return Redirect("/");
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(AuctionItemDto auctionItemDto)
        {
            List <string> errors = new List<string>();
            if (auctionItemDto.itemEndDate < (DateTime.Now.AddDays(1)))
            {
                errors.Add("Auction should end atleast one day from now.");
            }
            if (auctionItemDto.itemDescription == null)
            {
                errors.Add("Enter description.");
            }
            if(auctionItemDto.startingBid == null)
            {
                errors.Add("Enter starting bid.");
            }
            if (auctionItemDto.itemName == null)
            {
                errors.Add("Enter item name.");
            }
            if (auctionItemDto.ItemImages == null)
            {
                errors.Add("Enter iamge url.");
            }
            if (auctionItemDto.startingBid <=0)
            {
                errors.Add("Enter a valid starting bid");
            }
            if(errors.Any()){
                TempData["Error"] = errors;
                return RedirectToAction(nameof(Create));
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AuctionUser auctionUser = await userManager.FindByIdAsync(userId);
            await auctionItemService.CreateAuctionItem(auctionItemDto, auctionUser);

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
        [AllowAnonymous]
        public async Task<IActionResult> AuctionPage(long itemId) 
        {
            if (TempData.ContainsKey("Error"))
            {
                foreach(var error in TempData["Error"] as IEnumerable<string>)
                {
                    ModelState.AddModelError("bid", error.ToString());
                }
            }

            AuctionItemDto auctionItemDto = await auctionItemService.GetAuctionItemById(itemId);
            return View(auctionItemDto);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PlaceBid(long itemId, decimal bid)
        {
            if (signInManager.IsSignedIn(User))
            {
                AuctionItemDto auctionItem = await auctionItemService.GetAuctionItemById(itemId);
                if (bid <= auctionItem.currentBid)
                {
                    TempData["Error"] = new List<string> { "Bid amount must be greater than current price." };
                }
                else
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    AuctionUser auctionUser = await userManager.FindByIdAsync(userId);
                    auctionItemService.PlaceBid(itemId, bid,auctionUser);
                }
                return RedirectToAction(nameof(AuctionPage),new{itemId = itemId});
            }
            return Redirect("/");
        }
    }
}
