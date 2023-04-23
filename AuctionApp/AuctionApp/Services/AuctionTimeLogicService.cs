using AuctionApp.Controllers;
using AuctionApp.Data;
using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;
using AuctionApp.Services.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Xml.Linq;

namespace AuctionApp.Services
{
    public class AuctionTimeLogicService : IAuctionTimeLogicService
    {
        private Timer _timer;
        private readonly ILogger<AuctionTimeLogicService> _logger;
        private IAuctionItemService _itemService;
        private IServiceScopeFactory serviceScopeFactory;

        public AuctionTimeLogicService(ILogger<AuctionTimeLogicService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            this.serviceScopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            //_timer = new Timer(async o =>await EndAuctionManage(o), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            _timer = new Timer(EndAuctionManage, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        public void EndAuctionManage(object state)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                //auctionAppDbContext = scope.ServiceProvider.GetService<AuctionAppDbContext>();
                this._itemService = scope.ServiceProvider.GetService<IAuctionItemService>();
            }

            IQueryable<AuctionItemModel> auctions = _itemService.GetActiveAuctionItems();
            var now = DateTime.UtcNow;
            foreach (var auction in auctions)
            {
                if (auction.itemEndDate < now)
                {
                    AuctionUser winner = DetermineWinner(auction);
                    auction.isActive = false;
                    //auctionAppDbContext.Update(auction);
                    //auctionAppDbContext.SaveChanges();
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
            _logger.LogInformation("Timed Hosted Service is stopping.");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
