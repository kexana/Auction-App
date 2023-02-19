using AuctionApp.ModelDtos;

namespace AuctionApp.Services
{
    public interface IAuctionItemService
    {
        Task<AuctionItemDto> CreateAuctionItem(AuctionItemDto auctionItemDto);

        IQueryable<AuctionItemDto> GetAllAuctionItems(bool fetchDeleted = false);

        Task<AuctionItemDto> GetAuctionItemById(long id);

        Task<AuctionItemDto> UpdateAuctionItem(long id, AuctionItemDto auctionItemDto);

        Task<AuctionItemDto> DeleteAuctionItem(long id);
    }
}
