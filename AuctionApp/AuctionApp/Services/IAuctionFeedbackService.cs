using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;

namespace AuctionApp.Services
{
    public interface IAuctionFeedbackService
    {
        Task<AuctionFeedbackDto> CreateAuctionFeedback(AuctionFeedbackDto auctionFeedbackDto, AuctionUser auctionUser, AuctionItemModel auctionItem);

        IQueryable<AuctionFeedbackDto> GetAllAuctionFeedback(bool fetchDeleted = false);

        Task<AuctionFeedbackDto> GetAuctionFeedbackById(long id);

        IQueryable<AuctionFeedbackDto> GetAllAuctionFeedbackBySellerId(string userId);

        Task<AuctionFeedbackDto> UpdateAuctionFeedback(long id, AuctionFeedbackDto auctionFeedbackDto);

        Task<AuctionFeedbackDto> DeleteAuctionFeedback(long id);

        decimal CalculateRatingForSeller(string userId);
    }
}
