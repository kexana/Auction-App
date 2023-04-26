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
        public IQueryable<AuctionBidDto> GetAllAuctionBids(bool fetchDeleted = false)
        {
            IQueryable<AuctionBid> auctionBid = auctionDbContext.AuctionBids;

            return auctionBid.Select(bid => bid.ToDto(true, true));
        }
        public async Task<AuctionBidDto> GetAuctionBidById(long id)
        {
            AuctionBid auctionBid = await this.auctionDbContext.AuctionBids
               .Include(bid => bid.Bidder)
               .Include(bid => bid.Item)
               .SingleOrDefaultAsync(bid => bid.Id == id);

            if (auctionBid == null)
            {
                throw new ArgumentException("The Bid you are trying to get does not exist.");
            }

            AuctionBidDto auctionBidDto = auctionBid.ToDto();

            return auctionBidDto;
        }

        public IQueryable<AuctionBidDto> GetAllAuctionBidsByBidderId(string userId)
        {
            IQueryable<AuctionBid> auctionBid = auctionDbContext.AuctionBids;

            return auctionBid
                .Select(x => x.ToDto(true,true))
                .Where(bid => bid.BidderId == userId)
                .Include(bid => bid.Item);
        }

        public async Task<AuctionBidDto> UpdateAuctionBid(long id, AuctionBidDto auctionBidDto)
        {
            AuctionBid auctionBid = await this.auctionDbContext
                .AuctionBids
                .SingleOrDefaultAsync(bid => bid.Id == id);

            if (auctionBid == null)
            {
                throw new ArgumentException("The Bid you are trying to update does not exist.");
            }

            auctionBid.BidAmount = auctionBid.BidAmount;

            this.auctionDbContext.Update(auctionBid);
            await this.auctionDbContext.SaveChangesAsync();

            return auctionBid.ToDto();
        }

        public async Task<AuctionBidDto> DeleteAuctionBid(long id)
        {
            AuctionBid auctionBid = await this.auctionDbContext
                .AuctionBids
                .SingleOrDefaultAsync(bid => bid.Id == id);

            if (auctionBid == null)
            {
                throw new ArgumentException("The Bid you are trying to delete does not exist.");
            }

            this.auctionDbContext.AuctionBids.Remove(auctionBid);
            await this.auctionDbContext.SaveChangesAsync();

            return auctionBid.ToDto();
        }
    }
}
