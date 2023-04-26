using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;

namespace AuctionApp.ModelDtos
{
    public class AuctionBidDto
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public AuctionItemDto Item { get; set; }

        public string BidderId { get; set; }


        public AuctionUserDto Bidder { get; set; }

        public decimal BidAmount { get; set; }

        public DateTime? DateMade { get; set; }
    }
}
