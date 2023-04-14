using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AuctionApp.Data.Models
{
    public class AuctionItemModel
    {
        public int Id { get; set; }
        public string itemName { get; set; }

        public string itemDescription { get; set; }

        public string ItemImages  { get; set; }

        public ICollection<AuctionBid> Bids { get; set; }
        [AllowNull]
        public string ItemTags { get; set; }

        public DateTime? itemActivatedDate { get; set; }

        public DateTime? itemEndDate { get; set; }

        public bool isActive { get; set; }

        [ForeignKey("sellerUser")]
        [AllowNull]
        public string sellerUserId { get; set; }

        [AllowNull]
        public AuctionUser sellerUser { get; set; }

        [ForeignKey("buyerUser")]
        [AllowNull]
        public string? buyerUserId { get; set; }

        [AllowNull]
        public AuctionUser? buyerUser { get; set; }

    }
}
