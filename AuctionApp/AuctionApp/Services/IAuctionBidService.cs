using AuctionApp.ModelDtos;
using AuctionApp.Data.Models;

namespace AuctionApp.Services
{
    public interface IAuctionBidService
    {
        Task<AuctionBidDto> CreateAuctionBid(AuctionBidDto auctionItemDto, AuctionUser auctionUser);
    }
}
