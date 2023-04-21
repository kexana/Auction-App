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
    public interface IAuctionTimeLogicService : IHostedService, IDisposable 
    {
        Task StartAsync(CancellationToken stoppingToken);

        void EndAuctionManage(object state);

        AuctionUser DetermineWinner(AuctionItemModel item);

        Task StopAsync(CancellationToken stoppingToken);

        void Dispose();
    }
}
