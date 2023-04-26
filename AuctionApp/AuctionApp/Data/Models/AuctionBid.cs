using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AuctionApp.Data.Models
{
    public class AuctionBid
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public  AuctionItemModel Item { get; set; }

        [ForeignKey("Bidder")]
        public string BidderId { get; set; }

        public AuctionUser Bidder { get; set; }

        public decimal BidAmount { get; set; }

        public DateTime? DateMade { get; set; }

    }
}
