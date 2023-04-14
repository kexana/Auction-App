using AuctionApp.Controllers;
using AuctionApp.Data;
using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;
using AuctionApp.Services.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml.Linq;

namespace AuctionApp.Services
{
    public class AuctionBidService:IAuctionBidService
    {
        private AuctionAppDbContext auctionDbContext;

        public AuctionBidService(AuctionAppDbContext auctionDbContext)
        {
            this.auctionDbContext = auctionDbContext;
        }
        public async Task<AuctionBidDto> CreateAuctionBid(AuctionBidDto auctionBidDto, AuctionUser auctionUser)
        {
            auctionBidDto.DateMade = DateTime.Now;
            auctionBidDto.BidderId = auctionUser.Id;
            auctionBidDto.Bidder = auctionUser.ToDto();

            AuctionBid bid = auctionBidDto.ToEntity();

            await this.auctionDbContext.AuctionBids.AddAsync(bid);
            await this.auctionDbContext.SaveChangesAsync();

            return bid.ToDto();
        }
    }
}
