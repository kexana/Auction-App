using AuctionApp.Data;
using AuctionApp.Data.Models;
using AuctionApp.ModelDtos;
using AuctionApp.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AuctionApp.Tests
{
    public class AuctionItemServiceTests
    {
        private AuctionAppDbContext context;
        private AuctionItemService itemService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AuctionAppDbContext>()
                .UseInMemoryDatabase("TestDb").Options;

            this.context = new AuctionAppDbContext(options);
            itemService = new AuctionItemService(this.context);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Database.EnsureDeleted();
        }

        [Test]
        public void TestCreateAuctionItem()
        {
            AuctionItemDto item =CreateItemDto(1, "Item Name");
            AuctionUser user = new AuctionUser();

            itemService.CreateAuctionItem(item, user);

            AuctionItemModel dbItem = context.AuctionItems.FirstOrDefault();

            Assert.NotNull(dbItem);
        }

        [Test]
        public void TestEditAuctionItem()
        {
            AuctionItemDto item = CreateItemDto(1, "Item Name");
            AuctionUser user = new AuctionUser();

            itemService.CreateAuctionItem(item, user);

            AuctionItemDto editItem = new AuctionItemDto();

            editItem.Id = 1;
            editItem.itemName = "asd";

            itemService.UpdateAuctionItem(editItem.Id,editItem);

            AuctionItemModel dbItem = context.AuctionItems.FirstOrDefault(x => x.Id == 1);

            Assert.NotNull(dbItem);
            Assert.AreEqual(dbItem.itemName, "asd");
        }

        [Test]
        public void TestDeleteAuctionItem()
        {

            AuctionItemDto item = CreateItemDto(1, "Item Name");
            AuctionUser user = new AuctionUser();

            itemService.CreateAuctionItem(item, user);

            itemService.DeleteAuctionItem(1);

            AuctionItemModel dbItem = context.AuctionItems.FirstOrDefault(x => x.Id == 1);
            Assert.Null(dbItem);
        }

        [Test]
        public async Task TestGetAuctionItemById()
        {
            AuctionItemDto item = CreateItemDto(1, "Product Name");
            AuctionUser user = new AuctionUser();
            itemService.CreateAuctionItem(item, user);

            AuctionItemDto dbItem = await itemService.GetAuctionItemById(1);

            Assert.AreEqual(dbItem.itemName, "Product Name");
        }

        [Test]
        public void TestGetAllAuctionItems()
        {
            AuctionItemDto item1 = CreateItemDto(1, "Item Name");
            AuctionItemDto item2 = CreateItemDto(2, "Item Name2");
            AuctionItemDto item3 = CreateItemDto(3, "Item Name3");

            AuctionUser user = new AuctionUser();
            itemService.CreateAuctionItem(item1, user);
            itemService.CreateAuctionItem(item2, user);
            itemService.CreateAuctionItem(item3, user);

            List<AuctionItemDto> itemDtos = itemService.GetAllAuctionItems().ToList();

            Assert.AreEqual(3, itemDtos.Count);
            Assert.AreEqual("Item Name", itemDtos[0].itemName);
        }

        [Test]
        public void TestGetFilteredAuctionItems()
        {
            AuctionItemDto item1 = CreateItemDto(1, "Item Name");
            AuctionItemDto item2 = CreateItemDto(2, "Item Name2");
            AuctionItemDto item3 = CreateItemDto(3, "Item Name3");

            item2.isActive = false;

            AuctionUser user = new AuctionUser();

            itemService.CreateAuctionItem(item1, user);
            itemService.CreateAuctionItem(item2, user);
            itemService.CreateAuctionItem(item3, user);

            List<AuctionItemDto> itemDtos = itemService.GetFilteredAuctionItems().ToList();

            Assert.AreEqual(2,itemDtos.Count);
            Assert.AreEqual("Item Name3", itemDtos[0].itemName);
        }
        
        [Test]
        public void TestGetActiveAuctionItems()
        {
            AuctionItemDto item1 = CreateItemDto(1, "Item Name");
            AuctionItemDto item2 = CreateItemDto(2, "Item Name2");
            AuctionItemDto item3 = CreateItemDto(3, "Item Name3");

            item2.isActive = false;

            AuctionUser user = new AuctionUser();

            itemService.CreateAuctionItem(item1, user);
            itemService.CreateAuctionItem(item2, user);
            itemService.CreateAuctionItem(item3, user);

            List<AuctionItemModel> items = itemService.GetActiveAuctionItems().ToList();

            Assert.AreEqual(2, items.Count);
            Assert.AreEqual("Item Name", items[0].itemName);
        }

        [Test]
        public async Task TestGetAllAuctionItemsByUserId()
        {
            AuctionItemDto item1 = CreateItemDto(1, "Item Name");

            AuctionUser user = new AuctionUser();

            itemService.CreateAuctionItem(item1, user);

            IQueryable<AuctionItemDto> itemDtos = await itemService.GetAllAuctionItemsByUserId(user.Id);
            
            Assert.AreEqual("Item Name", itemDtos.First().itemName);
        }

        [Test]
        public async Task TestGetAllAuctionItemsByBidderId()
        {
            AuctionItemDto item1 = CreateItemDto(1, "Item Name");

            AuctionUser user = new AuctionUser();
            AuctionUserDto bidder = new AuctionUserDto();

            item1.buyerUser = bidder;
            item1.buyerUserId = bidder.Id;

            itemService.CreateAuctionItem(item1, user);

            IQueryable<AuctionItemDto> itemDtos = await itemService.GetAllAuctionItemsByBidderId(bidder.Id);

            Assert.AreEqual("Item Name", itemDtos.First().itemName);
        }

        [Test]
        public async Task TestPlaceBid()
        {
            AuctionItemDto item1 = CreateItemDto(1, "Item Name");

            AuctionUser user = new AuctionUser();

            decimal bid = 50;

            itemService.PlaceBid(item1.Id,bid, user);
            AuctionItemDto dbItem = await itemService.GetAuctionItemById(item1.Id);

            Assert.AreEqual(dbItem.Bids.FirstOrDefault().BidAmount, bid);
        }

        private AuctionItemDto CreateItemDto(int id, string name)
        {
            AuctionItemDto item = new AuctionItemDto();
            item.Id = id;
            item.itemName = name;

            return item;
        }
    }
}