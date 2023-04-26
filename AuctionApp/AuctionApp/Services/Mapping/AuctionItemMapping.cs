using AuctionApp.ModelDtos;
using AuctionApp.Data.Models;
using AuctionApp.Services.Mapping;

namespace AuctionApp.Services.Mapping
{
    public static class AuctionItemMapping
    {
        public static AuctionItemModel ToEntity(this AuctionItemDto auctionItemDto)
        {
            return new AuctionItemModel
            {
                Id = auctionItemDto.Id,
                itemName = auctionItemDto.itemName,
                itemDescription = auctionItemDto.itemDescription,
                ItemImages = auctionItemDto.ItemImages,
                ItemTags = auctionItemDto.ItemTags,
                itemActivatedDate = auctionItemDto.itemActivatedDate,
                itemEndDate = auctionItemDto.itemEndDate,
                isActive = auctionItemDto.isActive,
                buyerUserId = auctionItemDto.buyerUserId,
                sellerUserId = auctionItemDto.sellerUserId,
                Bids = true ? auctionItemDto.Bids?.Select(bid => bid.ToEntity()).ToList():null,
            };
        }

        public static AuctionItemDto ToDto(
            this AuctionItemModel auctionItem,
            bool fetchUser = true,
            bool fetchBids = true
            )
        {
            return new AuctionItemDto
            {
                Id = auctionItem.Id,
                itemName = auctionItem.itemName,
                itemDescription = auctionItem.itemDescription,
                ItemImages = auctionItem.ItemImages,
                ItemTags = auctionItem.ItemTags,
                itemActivatedDate = auctionItem.itemActivatedDate,
                itemEndDate = auctionItem.itemEndDate,
                isActive = auctionItem.isActive,
                Bids = fetchBids ? auctionItem.Bids?.Select(bid => bid.ToDto(fetchItem: false, fetchUser: false)).ToList():null,
                sellerUserId = auctionItem.sellerUserId,
                buyerUserId = auctionItem.buyerUserId,
                sellerUser = fetchUser ? auctionItem.sellerUser?.ToDto():null,
                buyerUser = fetchUser ? auctionItem.buyerUser?.ToDto():null,
            };
        }
    }
}
