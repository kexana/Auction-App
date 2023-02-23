using AuctionApp.Data.Models;
using System.Diagnostics.CodeAnalysis;

namespace AuctionApp.ModelDtos
{
    public class AuctionItemDto
    {
        public int Id { get; set; }
        public string itemName { get; set; }

        public string itemDescription { get; set; }

        public string ItemImages { get; set; }

        public string ItemTags { get; set; }

        public decimal startingBid { get; set; }

        public decimal currentBid { get; set; }

        public DateTime? itemActivatedDate { get; set; }

        public DateTime? itemEndDate { get; set; }

        public bool isActive { get; set; }

        public AuctionUserDto sellerUser { get; set; }

        public AuctionUserDto buyerUser { get; set; }
    }
}
