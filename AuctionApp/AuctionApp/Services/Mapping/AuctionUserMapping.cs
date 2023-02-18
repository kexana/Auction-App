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

        public static AuctionUserDto ToDto(this AuctionUser auctionUser)
        {
            return new AuctionUserDto
            {
                Id = auctionUser.Id,
                UserName = auctionUser.UserName,
                Email = auctionUser.Email
            };
        }
    }
}
