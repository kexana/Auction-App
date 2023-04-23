using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;

namespace AuctionApp.Services.Mapping
{
    public static class AuctionUserMapping
    {
        public static AuctionUser ToEntity(this AuctionUserDto auctionUserDto)
        {
            return new AuctionUser
            {
                Id = auctionUserDto.Id,
                UserName = auctionUserDto.UserName,
                Email = auctionUserDto.Email,
            };
        }

        public static AuctionUserDto ToDto(
            this AuctionUser auctionUser,
            bool fetchFeedback = true,
            bool fetchMessages =true
            )
        {
            return new AuctionUserDto
            {
                Id = auctionUser.Id,
                UserName = auctionUser.UserName,
                Email = auctionUser.Email,
                Feedback = fetchFeedback ? auctionUser.Feedback?.Select(feedback => feedback.ToDto(fetchUser: false)).ToList() : null,
                RecievedMessages = fetchMessages ? auctionUser.RecievedMessages.Select(message => message.ToDto(fetchUser: false)).ToList() : null,
                SentMessages = fetchMessages ? auctionUser.SentMessages.Select(message => message.ToDto(fetchUser: false)).ToList() : null, 
            };
        }
    }
}
