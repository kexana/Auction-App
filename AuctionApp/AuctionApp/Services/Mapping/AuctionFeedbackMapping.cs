using AuctionApp.ModelDtos;
using AuctionApp.Data.Models;
using AuctionApp.Services.Mapping;

namespace AuctionApp.Services.Mapping
{
    public static class AuctionFeedbackMapping
    {
        public static AuctionFeedback ToEntity(this AuctionFeedbackDto auctionFeedbackDto)
        {
            return new AuctionFeedback
            {
                Id = auctionFeedbackDto.Id,
                Rating = auctionFeedbackDto.Rating,
                FeedbackText = auctionFeedbackDto.FeedbackText,
                ItemId = auctionFeedbackDto.ItemId,
                ReviewerId = auctionFeedbackDto.ReviewerId,
            };
        }
        public static AuctionFeedbackDto ToDto(
            this AuctionFeedback auctionFeedback,
            bool fetchUser = true,
            bool fetchItem = true)
        {
            return new AuctionFeedbackDto
            {
                Id = auctionFeedback.Id,
                Rating = auctionFeedback.Rating,
                FeedbackText = auctionFeedback.FeedbackText,
                ItemId = auctionFeedback.ItemId,
                ReviewerId = auctionFeedback.ReviewerId,
                Reviewer = fetchUser ? auctionFeedback.Reviewer?.ToDto() : null,
                //Item = fetchItem ? auctionFeedback.Item?.ToDto() : null,
            };
        }
    }
}
