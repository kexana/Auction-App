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
                ItemImages= auctionItemDto.ItemImages,
                ItemTags= auctionItemDto.ItemTags,
                itemActivatedDate = auctionItemDto.itemActivatedDate,
                itemEndDate = auctionItemDto.itemEndDate,
                isActive = auctionItemDto.isActive,
                startingBid = auctionItemDto.startingBid,
                currentBid = auctionItemDto.currentBid,
                buyerUserId= auctionItemDto.buyerUserId,
                sellerUserId= auctionItemDto.sellerUserId,
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
                ItemImages = auctionItem.ItemImages,
                ItemTags = auctionItem.ItemTags,
                itemActivatedDate = auctionItem.itemActivatedDate,
                itemEndDate = auctionItem.itemEndDate,
                isActive = auctionItem.isActive,
                startingBid = auctionItem.startingBid,
                currentBid = auctionItem.currentBid,
                sellerUserId = auctionItem.sellerUserId,
            };
        }
    }
}
