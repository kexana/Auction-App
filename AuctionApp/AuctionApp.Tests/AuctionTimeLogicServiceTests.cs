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

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AuctionAppDbContext>()
                .UseInMemoryDatabase("TestDb").Options;

            this.context = new AuctionAppDbContext(options);
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
