namespace AuctionApp.Data.Models
{
    public class AuctionPrivateMessage
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime PostedDate { get; set; }

        public string SenderId { get; set; }

        public AuctionUser Sender { get; set; }

        public string RecepiantId { get; set; }

        public AuctionUser Recepiant { get; set; }

        public string Attachment { get; set; }

    }
}
