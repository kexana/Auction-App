using Microsoft.AspNetCore.Identity;

namespace AuctionApp.Data.Models
{
    public class AuctionUser : IdentityUser
    {
        public ICollection<AuctionFeedback> Feedback { get; set; }
    }
}
