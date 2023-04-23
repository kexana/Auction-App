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
using NUnit.Framework;

namespace AuctionApp.Tests
{
    public class AuctionBidServiceTests
    {
        private AuctionAppDbContext context;
        private AuctionBidService bidService;

        private static DbContextOptions<AuctionAppDbContext> options = new DbContextOptionsBuilder<AuctionAppDbContext>()
            .UseInMemoryDatabase("TestDb").Options;

        [OneTimeSetUp]
        public void Setup()
        {

            this.context = new AuctionAppDbContext(options);
            this.context.Database.EnsureCreated();

            bidService = new AuctionBidService(this.context);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Database.EnsureDeleted();
        }
        [Test]
        public void TestCreateAuctionBid()
        {
            AuctionBidDto bid = CreateBidDto(1,50);

            AuctionUser user = new AuctionUser();

            bidService.CreateAuctionBid(bid,user);

            AuctionBid dbBid = context.AuctionBids.FirstOrDefault();

            Assert.NotNull(dbBid);

        }

        [Test]
        public void TestUpdateAuctionBid()
        {
            AuctionBidDto bid = CreateBidDto(1, 50);
            AuctionUser user = new AuctionUser();

            bidService.CreateAuctionBid(bid, user);

            AuctionBidDto editBid = new AuctionBidDto();

            editBid.Id = 1;
            editBid.BidAmount = 100;

            bidService.UpdateAuctionBid(editBid.Id, editBid);

            AuctionBid dbBid = context.AuctionBids.FirstOrDefault(x => x.Id == 1);

            Assert.NotNull(dbBid);
            Assert.AreEqual(dbBid.BidAmount, 100);
        }

        [Test]
        public void TestDeleteAuctionBid() {

            AuctionBidDto bid = CreateBidDto(1, 50);
            AuctionUser user = new AuctionUser();

            bidService.CreateAuctionBid(bid, user);

            bidService.DeleteAuctionBid(1);

            AuctionBid dbBid = context.AuctionBids.FirstOrDefault(x => x.Id == 1);
            Assert.Null(dbBid);
        }

        [Test]
        public async Task TestGetAuctionBidById()
        {
            AuctionBidDto item = CreateBidDto(1, 50);
            AuctionUser user = new AuctionUser();

            bidService.CreateAuctionBid(item, user);

            AuctionBidDto dbBid = await bidService.GetAuctionBidById(1);

            Assert.AreEqual(dbBid.BidAmount, 50);
        }

        [Test]
        public void TestGetAllAuctionBids()
        {
            AuctionBidDto bid1 = CreateBidDto(1, 50);
            AuctionBidDto bid2 = CreateBidDto(2, 100);
            AuctionBidDto bid3 = CreateBidDto(3, 150);

            AuctionUser user = new AuctionUser();
            bidService.CreateAuctionBid(bid1, user);
            bidService.CreateAuctionBid(bid2, user);
            bidService.CreateAuctionBid(bid3, user);

            List<AuctionBidDto> bidDtos = bidService.GetAllAuctionBids().ToList();

            Assert.AreEqual(3, bidDtos.Count);
            Assert.AreEqual(50, bidDtos[0].BidAmount);
        }

        [Test]
        public void TestGetAllAuctionBidsByBidderId()
        {
            AuctionBidDto bid1 = CreateBidDto(1, 50);
            AuctionBidDto bid2 = CreateBidDto(2, 100);
            AuctionBidDto bid3 = CreateBidDto(3, 150);

            AuctionUser user = new AuctionUser();

            AuctionUserDto bidder = new AuctionUserDto();

            bid2.Bidder = bidder;
            bid2.BidderId = bidder.Id;

            bidService.CreateAuctionBid(bid1, user);
            bidService.CreateAuctionBid(bid2, user);
            bidService.CreateAuctionBid(bid3, user);

            List<AuctionBidDto> bidDtos = bidService.GetAllAuctionBidsByBidderId(bidder.Id).ToList();

            Assert.AreEqual(1, bidDtos.Count);
            Assert.AreEqual(100, bidDtos[0].BidAmount);
        }

        private AuctionBidDto CreateBidDto(int id, decimal amount)
        {
            AuctionBidDto bid = new AuctionBidDto();
            bid.Id = id;
            bid.BidAmount = amount;

            return bid;
        }
    }
}
