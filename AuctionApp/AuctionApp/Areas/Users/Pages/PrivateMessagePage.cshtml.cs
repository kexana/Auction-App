using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;
using AuctionApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

            IQueryable<AuctionPrivateMessageDto> pms = auctionPMService.GetAllAuctionPrivateMessagesByRecepiantId(userId);

            // Build account view model
            this.RecepiantUser = recepiantUser;
            this.PrivateMessages = pms;

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(AuctionPrivateMessageDto pmDto, string userId)
        {
            AuctionUser recepiantUser = await userManager.FindByIdAsync(userId);

            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AuctionUser senderUser = await userManager.FindByIdAsync(senderId);

            if (recepiantUser == null || senderId == null)
            {
                return NotFound();
            }
            if(pmDto.Attachment == null)
            {
                pmDto.Attachment = "empty";
            }

            if(senderUser.SentMessages==null)
            {
                senderUser.SentMessages = new List<AuctionPrivateMessage>();
                await userManager.UpdateAsync(senderUser);
            }
            if (recepiantUser.RecievedMessages==null)
            {
                recepiantUser.RecievedMessages = new List<AuctionPrivateMessage>();
                await userManager.UpdateAsync(recepiantUser);
            }

            await auctionPMService.CreateAuctionPrivateMessage(pmDto,senderUser,recepiantUser);

            IQueryable<AuctionPrivateMessageDto> pms = auctionPMService.GetAllAuctionPrivateMessagesByRecepiantId(userId);

            // Build account view model
            this.RecepiantUser = recepiantUser;
            this.PrivateMessages = pms;

            return Page();
        }
    }
}
