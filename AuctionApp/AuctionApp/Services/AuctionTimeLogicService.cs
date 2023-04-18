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
    public class AuctionTimeLogicService : IAuctionTimeLogicService
    {
        private Timer _timer;
        private AuctionItemService _itemService;
        private AuctionAppDbContext auctionAppDbContext;

        public AuctionTimeLogicService(AuctionAppDbContext auctionAppDbContext)
        {
            _timer = new Timer(EndAuctionManage, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            this.auctionAppDbContext= auctionAppDbContext;
            this._itemService = new AuctionItemService(auctionAppDbContext);
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        public void EndAuctionManage(object state)
        {
            // Check if any auctions have ended
            IQueryable<AuctionItemModel> auctions = _itemService.GetActiveAuctionItems();
            var now = DateTime.UtcNow;
            foreach (var auction in auctions)
            {
                if (auction.itemEndDate < now)
                {
                    // Auction has ended, determine the winner and perform any other actions
                    AuctionUser winner = DetermineWinner(auction);
                    auction.isActive = false;
                    auctionAppDbContext.Update(auction);
                    auctionAppDbContext.SaveChanges();
                }
            }
        }

        public AuctionUser DetermineWinner(AuctionItemModel item)
        {
            //TODO:More validations
            return item.Bids.Last().Bidder;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
