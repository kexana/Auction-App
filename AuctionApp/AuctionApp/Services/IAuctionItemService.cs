using AuctionApp.ModelDtos;
using AuctionApp.Data.Models;

namespace AuctionApp.Services
{
    public interface IAuctionItemService
    {
        Task<AuctionItemDto> CreateAuctionItem(AuctionItemDto auctionItemDto, AuctionUser auctionUser);

        IQueryable<AuctionItemDto> GetAllAuctionItems(bool fetchDeleted = false);

        IQueryable<AuctionItemDto> GetFilteredAuctionItems(bool fetchDeleted = false);

        Task<AuctionItemDto> GetAuctionItemById(long id);

        IQueryable<AuctionItemDto> GetAllAuctionItemsByUserId(string userId);

        Task<AuctionItemDto> UpdateAuctionItem(long id, AuctionItemDto auctionItemDto);

        Task<AuctionItemDto> DeleteAuctionItem(long id);

        Task<AuctionItemDto> PlaceBid(long itemId, decimal bid, AuctionUser auctionUser);
    }
}
