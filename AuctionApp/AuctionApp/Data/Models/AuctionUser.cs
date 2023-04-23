using Microsoft.AspNetCore.Identity;

namespace AuctionApp.Data.Models
{
    public class AuctionUser : IdentityUser
    {
        public ICollection<AuctionFeedback> Feedback { get; set; }

        public ICollection<AuctionPrivateMessage> SentMessages { get; set; }

        public ICollection<AuctionPrivateMessage> RecievedMessages { get; set; }
    }
}
