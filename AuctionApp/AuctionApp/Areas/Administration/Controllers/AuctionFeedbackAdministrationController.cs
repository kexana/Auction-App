using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;
using AuctionApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.Areas.Administration.Controllers
{
    [Route("/Administration/AuctionFeedback")]

    public class AuctionFeedbackAdministrationController : BaseAdministrationController
    {
        private readonly IAuctionFeedbackService auctionFeedbackService;
        private readonly SignInManager<AuctionUser> signInManager;
        private readonly UserManager<AuctionUser> userManager;

        public AuctionFeedbackAdministrationController(IAuctionFeedbackService auctionFeedbackService, SignInManager<AuctionUser> signInManager, UserManager<AuctionUser> userManager)
        {
            this.auctionFeedbackService = auctionFeedbackService;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(long id)
        {
            AuctionFeedbackDto auctionFeedbackDto = await auctionFeedbackService.GetAuctionFeedbackById(id);
            return View(auctionFeedbackDto);
        }
        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(long id, AuctionFeedbackDto auctionFeedbackDto)
        {
            await auctionFeedbackService.UpdateAuctionFeedback(id, auctionFeedbackDto);

            return Redirect("/Administration/Home/FeedbackIndex");
        }
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            AuctionFeedbackDto auctionFeedbackDto = await auctionFeedbackService.GetAuctionFeedbackById(id);
            return View(auctionFeedbackDto);
        }
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(long id, AuctionFeedbackDto auuctionFeedbackDto)
        {
            await auctionFeedbackService.DeleteAuctionFeedback(id);

            return Redirect("/Administration/Home/FeedbackIndex");
        }
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(long id)
        {
            AuctionFeedbackDto auctionFeedbackDto = await auctionFeedbackService.GetAuctionFeedbackById(id);
            return View(auctionFeedbackDto);
        }
    }
}
