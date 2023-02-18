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
                itemActivatedDate = auctionItemDto.itemActivatedDate,
                itemEndDate = auctionItemDto.itemEndDate,
                isActive = auctionItemDto.isActive,
                startingBid = auctionItemDto.startingBid,
                currentBid = auctionItemDto.currentBid,
            };
        }

        public static AuctionItemDto ToDto(
            this AuctionItemModel auctionItem,
            bool fetchUser = true)
        {
            return new AuctionItemDto
            {
                Id = auctionItem.Id,
                itemName = auctionItem.itemName,
                itemDescription = auctionItem.itemDescription,
                itemActivatedDate = auctionItem.itemActivatedDate,
                itemEndDate = auctionItem.itemEndDate,
                isActive = auctionItem.isActive,
                startingBid = auctionItem.startingBid,
                currentBid = auctionItem.currentBid,
                buyerUser = fetchUser ? auctionItem.buyerUser?.ToDto() : null,
                sellerUser = fetchUser ? auctionItem.sellerUser?.ToDto() : null,
            };
        }
    }
}
