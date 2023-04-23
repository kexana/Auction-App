using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionApp.Data;
using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;
using AuctionApp.Services;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Tests
{
    public class AuctionTimeLogicServiceTests
    {
        private AuctionAppDbContext context;
        private AuctionItemService ItemService;
        private static DbContextOptions<AuctionAppDbContext> options = new DbContextOptionsBuilder<AuctionAppDbContext>()
                .UseInMemoryDatabase("TestDb").Options;

        [OneTimeSetUp]
        public void Setup()
        {
            

            this.context = new AuctionAppDbContext(options);
            this.context.Database.EnsureCreated();

            ItemService = new AuctionItemService(this.context);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Database.EnsureDeleted();
        }

        [Test]
        public void TestEndAuctionManage()
        {

        }
    }
}
