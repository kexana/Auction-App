using AuctionApp.Data;
using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;
using AuctionApp.Services.Mapping;

namespace AuctionApp.Services
{
    public class AuctionItemService : IAuctionItemService
    {
        private AuctionAppDbContext auctionDbContext;

        public AuctionItemService(AuctionAppDbContext auctionDbContext)
        {
            this.auctionDbContext = auctionDbContext;
        }

        public Task<AuctionItemDto> CreateForumSection(AuctionItemDto auctionItemDto)
        {
            throw new NotImplementedException();
        }

        public Task<AuctionItemDto> DeleteForumSection(long id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AuctionItemDto> GetAllForumSections(bool fetchDeleted = false)
        {
            IQueryable<AuctionItemModel> auctionItems = auctionDbContext.AuctionItems;

            return auctionItems.Select(item=>item.ToDto(true));
        }

        public Task<AuctionItemDto> GetForumSectionById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<AuctionItemDto> UpdateForumSection(long id, AuctionItemDto auctionItemDto)
        {
            throw new NotImplementedException();
        }
    }
}
