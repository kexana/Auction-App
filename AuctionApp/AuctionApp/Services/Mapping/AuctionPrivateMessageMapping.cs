using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;

namespace AuctionApp.Services.Mapping
{
    public static class AuctionPrivateMessageMapping
    {
        public static AuctionPrivateMessage ToEntity(this AuctionPrivateMessageDto auctionPMDto)
        {
            return new AuctionPrivateMessage
            {
                Id = auctionPMDto.Id,
                Text = auctionPMDto.Text,
                Attachment = auctionPMDto.Attachment,
                SenderId= auctionPMDto.SenderId,
                RecepiantId = auctionPMDto.RecepiantId,
            };
        }

        public static AuctionPrivateMessageDto ToDto(
            this AuctionPrivateMessage auctionPM,
            bool fetchUser = true
            )
        {
            return new AuctionPrivateMessageDto
            {
                Id = auctionPM.Id,
                Text = auctionPM.Text,
                Attachment = auctionPM.Attachment,
                PostedDate = auctionPM.PostedDate,
                SenderId = auctionPM.SenderId,
                Sender = fetchUser ? auctionPM.Sender?.ToDto(fetchMessages: false) : null,
                RecepiantId = auctionPM.RecepiantId,
                Recepiant = fetchUser ? auctionPM.Recepiant?.ToDto(fetchMessages: false) : null,
                
            };
        }
    }
}
