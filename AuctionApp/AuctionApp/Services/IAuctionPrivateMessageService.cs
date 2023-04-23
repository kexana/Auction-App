using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;

namespace AuctionApp.Services
{
    public interface IAuctionPrivateMessageService
    {
        Task<AuctionPrivateMessageDto> CreateAuctionPrivateMessage(AuctionPrivateMessageDto auctionPrivateMessageDto, AuctionUser senderUser, AuctionUser recepiantUser);

        IQueryable<AuctionPrivateMessageDto> GetAllAuctionPrivateMessages(bool fetchDeleted = false);

        Task<AuctionPrivateMessageDto> GetAuctionPrivateMessageById(long id);

        IQueryable<AuctionPrivateMessageDto> GetAllAuctionPrivateMessagesBySenderId(string userId);

        IQueryable<AuctionPrivateMessageDto> GetAllAuctionPrivateMessagesByRecepiantId(string userId);

        IQueryable<AuctionPrivateMessageDto> GetAllAuctionPrivateMessagesBetweenSenderAndRecepiant(string senderId, string recepiantId);

        IQueryable<AuctionUserDto> GetAllAuctionUserWithCorrespondance(string senderId);

        Task<AuctionPrivateMessageDto> UpdateAuctionPrivateMessage(long id, AuctionPrivateMessageDto auctionPrivateMessageDto);

        Task<AuctionPrivateMessageDto> DeleteAuctionPrivateMessage(long id);
    }
}
