namespace AuctionApp.Data.Models
{
    public class AuctionItemModel
    {
        public int Id { get; set; }
        public string itemName { get; set; }

        public string itemDescription { get; set; }

        public decimal startingBid { get; set; }

        public decimal currentBid { get; set; }

        public DateTime? itemActivatedDate { get; set; }

        public DateTime? itemEndDate { get; set; }

        public bool isActive { get; set; }

        public AuctionUser sellerUser { get; set; }

        public AuctionUser buyerUser { get; set; }
    }
}
