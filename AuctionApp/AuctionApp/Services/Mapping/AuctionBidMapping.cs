using AuctionApp.ModelDtos;
using AuctionApp.Data.Models;
using AuctionApp.Services.Mapping;

namespace AuctionApp.Services.Mapping
{
    public static class AuctionBidMapping
    {

        public static AuctionBid ToEntity(this AuctionBidDto auctionBidDto)
        {
            return new AuctionBid
            {
                Id = auctionBidDto.Id,
                DateMade = auctionBidDto.DateMade,
                BidAmount = auctionBidDto.BidAmount,
                BidderId = auctionBidDto.BidderId,
                ItemId= auctionBidDto.ItemId,
            };
        }
        public static AuctionBidDto ToDto(
            this AuctionBid auctionBid,
            bool fetchUser = true,
            bool fetchItem = true)
        {
            return new AuctionBidDto
            {
                Id = auctionBid.Id,
                DateMade = auctionBid.DateMade,
                BidAmount = auctionBid.BidAmount,
                BidderId = auctionBid.BidderId,
                ItemId = auctionBid.ItemId,
                Item = fetchItem ? auctionBid.Item?.ToDto(fetchBids: false) : null,
                Bidder = fetchUser ? auctionBid.Bidder?.ToDto() : null,
            };
        }
    }
}
