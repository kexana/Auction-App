using AuctionApp.Data.Models;

namespace AuctionApp.ModelDtos
{
    public class AuctionPrivateMessageDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime PostedDate { get; set; }

        public string SenderId { get; set; }

        public AuctionUserDto Sender { get; set; }

        public string RecepiantId { get; set; }

        public AuctionUserDto Recepiant { get; set; }

        public string Attachment { get; set; }
    }
}
