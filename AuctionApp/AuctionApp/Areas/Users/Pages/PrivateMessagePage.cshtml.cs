using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;
using AuctionApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Areas.Users.Pages
{
    public class PrivateMessagePageModel : PageModel
    {
        private readonly IAuctionPrivateMessageService auctionPMService;
        private readonly SignInManager<AuctionUser> signInManager;
        private readonly UserManager<AuctionUser> userManager;

        public IQueryable<AuctionPrivateMessageDto> PrivateMessages { get; private set; }
        public AuctionUser RecepiantUser { get; private set; }

        public PrivateMessagePageModel(IAuctionPrivateMessageService auctionPMService, SignInManager<AuctionUser> signInManager, UserManager<AuctionUser> userManager)
        {
            this.auctionPMService = auctionPMService;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync(string userId)
        {
            AuctionUser recepiantUser = await userManager.FindByIdAsync(userId);

            if (recepiantUser == null)
            {
                return NotFound();
            }

            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AuctionUser senderUser = await userManager.Users
                .Include(u => u.SentMessages)
                .Include(u => u.RecievedMessages)
                .FirstOrDefaultAsync(u => u.Id == senderId);

            IQueryable<AuctionPrivateMessageDto> pms = auctionPMService.GetAllAuctionPrivateMessagesBetweenSenderAndRecepiant(senderId, userId);

            // Build account view model
            this.RecepiantUser = recepiantUser;
            this.PrivateMessages = pms;

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(AuctionPrivateMessageDto pmDto, string userId)
        {
            AuctionUser recepiantUser = await userManager.Users
                .Include(u=>u.SentMessages)
                .Include(u=>u.RecievedMessages)
                .FirstOrDefaultAsync(u=>u.Id==userId);

            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AuctionUser senderUser = await userManager.Users
                .Include(u => u.SentMessages)
                .Include(u => u.RecievedMessages)
                .FirstOrDefaultAsync(u => u.Id == senderId);

            if (recepiantUser == null || senderId == null)
            {
                return NotFound();
            }
            if(pmDto.Attachment == null)
            {
                pmDto.Attachment = "empty";
            }

            await auctionPMService.CreateAuctionPrivateMessage(pmDto,senderUser,recepiantUser);

            IQueryable<AuctionPrivateMessageDto> pms = auctionPMService.GetAllAuctionPrivateMessagesBetweenSenderAndRecepiant(senderId,userId);

            // Build account view model
            this.RecepiantUser = recepiantUser;
            this.PrivateMessages = pms;

            return Page();
        }
    }
}
