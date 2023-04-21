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
    public class AuctionItemService : IAuctionItemService
    {
        private AuctionAppDbContext auctionDbContext;

        public AuctionItemService(AuctionAppDbContext auctionDbContext)
        {
            this.auctionDbContext = auctionDbContext;
        }

        public async Task<AuctionItemDto> CreateAuctionItem(AuctionItemDto auctionItemDto, AuctionUser auctionUser)
        {
            auctionItemDto.isActive= true;
            auctionItemDto.itemActivatedDate = DateTime.Now;
            auctionItemDto.buyerUserId = null;

            AuctionItemModel auctionItem = auctionItemDto.ToEntity();

            auctionItem.sellerUser = auctionUser;
            auctionItem.buyerUser = null;
            auctionItem.Bids = new List<AuctionBid>();

            await this.auctionDbContext.AuctionItems.AddAsync(auctionItem);
            await this.auctionDbContext.SaveChangesAsync();

            return auctionItem.ToDto();
        }

        public async Task<AuctionItemDto> DeleteAuctionItem(long id)
        {
            AuctionItemModel auctionItem = await this.auctionDbContext
                .AuctionItems
                .SingleOrDefaultAsync(item => item.Id == id);

            if (auctionItem == null)
            {
                throw new ArgumentException("The item you are trying to delete does not exist.");
            }

            this.auctionDbContext.AuctionItems.Remove(auctionItem);
            await this.auctionDbContext.SaveChangesAsync();

            return auctionItem.ToDto();
        }

        public IQueryable<AuctionItemDto> GetAllAuctionItems(bool fetchDeleted = false)
        {
            IQueryable<AuctionItemModel> auctionItems = auctionDbContext.AuctionItems.Include(x=>x.Bids);

            return auctionItems.Select(item=>item.ToDto(true,true));
        }

        public IQueryable<AuctionItemDto> GetFilteredAuctionItems(bool fetchDeleted = false)
        {
            IQueryable<AuctionItemModel> auctionItems = auctionDbContext.AuctionItems.Include(x => x.Bids);

            return auctionItems.Where(item => item.isActive).OrderByDescending(item => item.itemActivatedDate).Select(item => item.ToDto(true, true));
        }

        public IQueryable<AuctionItemModel> GetActiveAuctionItems(bool fetchDeleted = false)
        {
            IQueryable<AuctionItemModel> auctionItems = auctionDbContext.AuctionItems.Include(x => x.Bids);

            return auctionItems.Where(item => item.isActive);
        }

        public async Task<AuctionItemDto> GetAuctionItemById(long id)
        {
            AuctionItemModel auctionItem = await this.auctionDbContext.AuctionItems
               .Include(item => item.sellerUser)
               .Include(item => item.buyerUser )
               .Include(item => item.Bids )
               .SingleOrDefaultAsync(item => item.Id == id);

            if (auctionItem == null)
            {
                throw new ArgumentException("The item you are trying to get does not exist.");
            }

            AuctionItemDto auctionItemDto = auctionItem.ToDto();

            return auctionItemDto;
        }

        public async Task<IQueryable<AuctionItemDto>> GetAllAuctionItemsByUserId(string userId)
        {
            IQueryable<AuctionItemModel> auctionItems = auctionDbContext.AuctionItems.Include(x=>x.Bids);

            return auctionItems
                .Where(item => item.isActive && item.sellerUserId==userId)
                .OrderByDescending(item => item.itemActivatedDate)
                .Select(item => item.ToDto(true, true));
        }

        public async Task<IQueryable<AuctionItemDto>> GetAllAuctionItemsByBidderId(string userId)
        {
            IQueryable<AuctionItemModel> auctionItems = auctionDbContext.AuctionItems.Include(x => x.Bids);

            return auctionItems
                .Where(item => item.Bids.OrderBy(b => b.BidAmount).Last().BidderId == userId)
                .OrderByDescending(item => item.itemActivatedDate)
                .Select(item => item.ToDto(true, true));
        }

        public async Task<AuctionItemDto> UpdateAuctionItem(long id, AuctionItemDto auctionItemDto)
        {
            AuctionItemModel auctionItem = await this.auctionDbContext
                .AuctionItems
                .SingleOrDefaultAsync(item => item.Id == id);

            if (auctionItem == null)
            {
                throw new ArgumentException("The item you are trying to update does not exist.");
            }

            auctionItem.itemName = auctionItemDto.itemName;
            auctionItem.itemDescription = auctionItemDto.itemDescription;
            auctionItem.isActive = auctionItemDto.isActive;
            foreach (AuctionBid bid in auctionItem.Bids)
            {
                auctionItemDto.Bids[bid.Id]=bid.ToDto(true);
            }

            this.auctionDbContext.Update(auctionItem);
            await this.auctionDbContext.SaveChangesAsync();

            return auctionItem.ToDto();
        }

        public async Task<AuctionItemDto> PlaceBid(long itemId,decimal bid, AuctionUser auctionUser)
        {
            AuctionItemModel auctionItem = await this.auctionDbContext
                .AuctionItems
                .SingleOrDefaultAsync(item => item.Id == itemId);

            if (auctionItem == null)
            {
                throw new ArgumentException("The item you are trying to update does not exist.");
            }

            AuctionBid newBid = new AuctionBid();
            newBid.BidAmount = bid;
            newBid.DateMade = DateTime.Now;
            newBid.Bidder = auctionUser;
            newBid.BidderId = auctionUser.Id;
            newBid.ItemId = (int)itemId;

            auctionItem.Bids.Add(newBid);
            auctionItem.buyerUser = auctionUser;
            auctionItem.buyerUserId = auctionUser.Id;

            try
            {
                this.auctionDbContext.Update(auctionItem);
                this.auctionDbContext.AuctionBids.Add(newBid);
                await this.auctionDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception here
            }

            return auctionItem.ToDto();
        }
    }
}
