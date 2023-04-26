using AuctionApp.Data.Models;

namespace AuctionApp.ModelDtos
{
    public class AuctionFeedbackDto
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public string FeedbackText { get; set; }

        public int ItemId { get; set; }

        public AuctionItemDto Item { get; set; }

        public string ReviewerId { get; set; }

        public AuctionUserDto Reviewer { get; set; }
    }
}
