using AuctionApp.ModelDtos;
using AuctionApp.Data.Models;

namespace AuctionApp.Services
{
    public interface IAuctionBidService
    {
        Task<AuctionBidDto> CreateAuctionBid(AuctionBidDto auctionItemDto, AuctionUser auctionUser);
        IQueryable<AuctionBidDto> GetAllAuctionBids(bool fetchDeleted = false);

        Task<AuctionBidDto> GetAuctionBidById(long id);

        IQueryable<AuctionBidDto> GetAllAuctionBidsByBidderId(string userId);

        Task<AuctionBidDto> UpdateAuctionBid(long id, AuctionBidDto auctionBidDto);

        Task<AuctionBidDto> DeleteAuctionBid(long id);
    }
}
