using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;
using AuctionApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.Areas.Administration.Controllers
{
    [Route("/Administration/AuctionBids")]

    public class AuctionBidAdministrationController : BaseAdministrationController
    {
        private readonly IAuctionBidService auctionBidService;
        private readonly SignInManager<AuctionUser> signInManager;
        private readonly UserManager<AuctionUser> userManager;

        public AuctionBidAdministrationController(IAuctionBidService auctionBidService, SignInManager<AuctionUser> signInManager, UserManager<AuctionUser> userManager)
        {
            this.auctionBidService = auctionBidService;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(long id)
        {
            AuctionBidDto auctionBidDto = await auctionBidService.GetAuctionBidById(id);
            return View(auctionBidDto);
        }
        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(long id, AuctionBidDto auctionBidDto)
        {
            await auctionBidService.UpdateAuctionBid(id, auctionBidDto);

            return Redirect("/Administration/Home/BidsIndex");
        }
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            AuctionBidDto auctionBidDto = await auctionBidService.GetAuctionBidById(id);
            return View(auctionBidDto);
        }
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(long id, AuctionBidDto auuctionBidDto)
        {
            await auctionBidService.DeleteAuctionBid(id);

            return Redirect("/Administration/Home/BidsIndex");
        }
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(long id)
        {
            AuctionBidDto auctionBidDto = await auctionBidService.GetAuctionBidById(id);
            return View(auctionBidDto);
        }
    }
}
