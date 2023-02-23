using AuctionApp.Data;
using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;
using AuctionApp.Services.Mapping;
using Microsoft.AspNetCore.Identity;
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

        public async Task<AuctionItemDto> CreateAuctionItem(AuctionItemDto auctionItemDto, AuctionUserDto auctionUserDto)
        {
            auctionItemDto.isActive= true;
            auctionItemDto.currentBid = auctionItemDto.startingBid;
            auctionItemDto.itemActivatedDate = DateTime.Now;
            auctionItemDto.sellerUser = auctionUserDto;

            AuctionItemModel auctionItem = auctionItemDto.ToEntity();

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
            IQueryable<AuctionItemModel> auctionItems = auctionDbContext.AuctionItems;

            return auctionItems.Select(item=>item.ToDto(true));
        }

        public IQueryable<AuctionItemDto> GetFilteredAuctionItems(bool fetchDeleted = false)
        {
            IQueryable<AuctionItemModel> auctionItems = auctionDbContext.AuctionItems;

            return auctionItems.Where(item => item.isActive).OrderByDescending(item => item.itemActivatedDate).Select(item => item.ToDto(true));
        }

        public async Task<AuctionItemDto> GetAuctionItemById(long id)
        {
            AuctionItemModel auctionItem = await this.auctionDbContext.AuctionItems
               .Include(item => item.sellerUser)
               .Include(item => item.buyerUser )
               .SingleOrDefaultAsync(item => item.Id == id);

            if (auctionItem == null)
            {
                throw new ArgumentException("The item you are trying to delete does not exist.");
            }

            AuctionItemDto auctionItemDto = auctionItem.ToDto();

            return auctionItemDto;
        }

        public async Task<AuctionItemDto> UpdateAuctionItem(long id, AuctionItemDto auctionItemDto)
        {
            AuctionItemModel auctionItem = await this.auctionDbContext
                .AuctionItems
                .SingleOrDefaultAsync(item => item.Id == id);

            if (auctionItem == null)
            {
                throw new ArgumentException("The item you are trying to delete does not exist.");
            }

            auctionItem.itemName = auctionItemDto.itemName;
            auctionItem.itemDescription = auctionItemDto.itemDescription;
            auctionItem.isActive = auctionItemDto.isActive;
            auctionItem.currentBid = auctionItemDto.currentBid;

            this.auctionDbContext.Update(auctionItem);
            await this.auctionDbContext.SaveChangesAsync();

            return auctionItem.ToDto();
        }
    }
}
