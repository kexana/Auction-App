using AuctionApp.ModelDtos;

namespace AuctionApp.Services
{
    public interface IAuctionItemService
    {
        Task<AuctionItemDto> CreateForumSection(AuctionItemDto auctionItemDto);

        IQueryable<AuctionItemDto> GetAllForumSections(bool fetchDeleted = false);

        Task<AuctionItemDto> GetForumSectionById(long id);

        Task<AuctionItemDto> UpdateForumSection(long id, AuctionItemDto auctionItemDto);

        Task<AuctionItemDto> DeleteForumSection(long id);
    }
}
