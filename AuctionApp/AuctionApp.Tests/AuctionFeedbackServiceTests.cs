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
    public class AuctionFeedbackServiceTests
    {
        private AuctionAppDbContext context;
        private AuctionFeedbackService feedbackService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AuctionAppDbContext>()
                .UseInMemoryDatabase("TestDb").Options;

            this.context = new AuctionAppDbContext(options);
            feedbackService = new AuctionFeedbackService(this.context);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Database.EnsureDeleted();
        }

        [Test]
        public async Task TestCreateAuctionFeedback()
        {
            AuctionFeedbackDto feedback = CreateFeedbackDto(1, "Test");

            AuctionUser user = new AuctionUser();

            AuctionItemModel item = new AuctionItemModel();

            await feedbackService.CreateAuctionFeedback(feedback, user, item);

            AuctionBid dbBid = context.AuctionBids.FirstOrDefault();

            Assert.NotNull(dbBid);
        }

        [Test]
        public void TestUpdateAuctionFeedback()
        {
            AuctionFeedbackDto feedback = CreateFeedbackDto(1, "Test");

            AuctionUser user = new AuctionUser();

            AuctionItemModel item = new AuctionItemModel();

            feedbackService.CreateAuctionFeedback(feedback, user, item);

            AuctionFeedbackDto editFeedback = new AuctionFeedbackDto();

            editFeedback.Id = 1;
            editFeedback.FeedbackText = "asdf";

            feedbackService.UpdateAuctionFeedback(editFeedback.Id, editFeedback);

            AuctionFeedback dbFeedback = context.AuctionFeedback.FirstOrDefault(x => x.Id == 1);

            Assert.NotNull(dbFeedback);
            Assert.AreEqual(dbFeedback.FeedbackText, "asdf");
        }

        [Test]
        public void TestDeleteAuctionBid()
        {

            AuctionFeedbackDto feedback = CreateFeedbackDto(1, "Test");

            AuctionUser user = new AuctionUser();

            AuctionItemModel item = new AuctionItemModel();

            feedbackService.CreateAuctionFeedback(feedback, user, item);

            feedbackService.DeleteAuctionFeedback(1);

            AuctionFeedback dbFeedback = context.AuctionFeedback.FirstOrDefault(x => x.Id == 1);
            Assert.Null(dbFeedback);
        }

        [Test]
        public async Task TestGetAuctionFeedbackById()
        {
            AuctionFeedbackDto feedback = CreateFeedbackDto(1, "Test");

            AuctionUser user = new AuctionUser();

            AuctionItemModel item = new AuctionItemModel();

            feedbackService.CreateAuctionFeedback(feedback, user,item);

            AuctionFeedbackDto dbFeedback = await feedbackService.GetAuctionFeedbackById(1);

            Assert.AreEqual(dbFeedback.FeedbackText, "Test");
        }

        [Test]
        public void TestGetAllAuctionFeedback()
        {
            AuctionFeedbackDto feedback1 = CreateFeedbackDto(1, "Test1");
            AuctionFeedbackDto feedback2 = CreateFeedbackDto(2, "Test2");
            AuctionFeedbackDto feedback3 = CreateFeedbackDto(3, "Test3");

            AuctionUser user = new AuctionUser();

            AuctionItemModel item = new AuctionItemModel();

            feedbackService.CreateAuctionFeedback(feedback1, user, item);
            feedbackService.CreateAuctionFeedback(feedback2, user, item);
            feedbackService.CreateAuctionFeedback(feedback3, user, item);

            List<AuctionFeedbackDto> feedbackDtos = feedbackService.GetAllAuctionFeedback().ToList();

            Assert.AreEqual(3, feedbackDtos.Count);
            Assert.AreEqual("Test1", feedbackDtos[0].FeedbackText);
        }

        [Test]
        public void TestGetAllAuctionFeedbackBySellerId()
        {
            AuctionFeedbackDto feedback1 = CreateFeedbackDto(1, "Test1");
            AuctionFeedbackDto feedback2 = CreateFeedbackDto(2, "Test2");
            AuctionFeedbackDto feedback3 = CreateFeedbackDto(3, "Test3");

            AuctionUser user = new AuctionUser();

            AuctionUser seller = new AuctionUser();

            AuctionItemModel item = new AuctionItemModel();

            item.sellerUser= seller;
            item.sellerUserId = seller.Id;

            feedbackService.CreateAuctionFeedback(feedback1, user, item);
            feedbackService.CreateAuctionFeedback(feedback2, user, item);
            feedbackService.CreateAuctionFeedback(feedback3, user, item);

            List<AuctionFeedbackDto> feedbackDtos = feedbackService.GetAllAuctionFeedbackBySellerId(seller.Id).ToList();

            Assert.AreEqual(3, feedbackDtos.Count);
            Assert.AreEqual("Test1", feedbackDtos[0].FeedbackText);
        }

        [Test]
        public void TestCalculateRatingForSeller()
        {
            AuctionFeedbackDto feedback1 = CreateFeedbackDto(1, "Test1");
            AuctionFeedbackDto feedback2 = CreateFeedbackDto(2, "Test2");
            AuctionFeedbackDto feedback3 = CreateFeedbackDto(3, "Test3");

            feedback1.Rating = 1;
            feedback2.Rating= 2;
            feedback3.Rating= 3;

            AuctionUser user = new AuctionUser();

            AuctionUser seller = new AuctionUser();

            AuctionItemModel item = new AuctionItemModel();

            item.sellerUser = seller;
            item.sellerUserId = seller.Id;

            feedbackService.CreateAuctionFeedback(feedback1, user, item);
            feedbackService.CreateAuctionFeedback(feedback2, user, item);
            feedbackService.CreateAuctionFeedback(feedback3, user, item);

            Assert.AreEqual(2, feedbackService.CalculateRatingForSeller(seller.Id));
        }

        private AuctionFeedbackDto CreateFeedbackDto(int id, string text)
        {
            AuctionFeedbackDto feedback = new AuctionFeedbackDto();
            feedback.Id = id;
            feedback.FeedbackText = text;

            return feedback;
        }
    }
}
