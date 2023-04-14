﻿using AuctionApp.Controllers;
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
    public class AuctionFeedbackService : IAuctionFeedbackService
    {
        private AuctionAppDbContext auctionDbContext;

        public AuctionFeedbackService(AuctionAppDbContext auctionDbContext)
        {
            this.auctionDbContext = auctionDbContext;
        }

        public async Task<AuctionFeedbackDto> CreateAuctionFeedback(AuctionFeedbackDto auctionFeedbackDto, AuctionUser auctionUser)
        {

            AuctionFeedback auctionFeedback = auctionFeedbackDto.ToEntity();

            auctionFeedback.Reviewer = auctionUser;

            await this.auctionDbContext.AuctionFeedback.AddAsync(auctionFeedback);
            await this.auctionDbContext.SaveChangesAsync();

            return auctionFeedback.ToDto();
        }

        public async Task<AuctionFeedbackDto> DeleteAuctionFeedback(long id)
        {
            AuctionFeedback auctionFeedback = await this.auctionDbContext
                .AuctionFeedback
                .SingleOrDefaultAsync(feedback => feedback.Id == id);

            if (auctionFeedback == null)
            {
                throw new ArgumentException("The Feedback you are trying to delete does not exist.");
            }

            this.auctionDbContext.AuctionFeedback.Remove(auctionFeedback);
            await this.auctionDbContext.SaveChangesAsync();

            return auctionFeedback.ToDto();
        }

        public IQueryable<AuctionFeedbackDto> GetAllAuctionFeedback(bool fetchDeleted = false)
        {
            IQueryable<AuctionFeedback> auctionFeedback = auctionDbContext.AuctionFeedback;

            return auctionFeedback.Select(feedback => feedback.ToDto(true,true));
        }

        public async Task<AuctionFeedbackDto> GetAuctionFeedbackById(long id)
        {
            AuctionFeedback auctionFeedback = await this.auctionDbContext.AuctionFeedback
               .Include(feedback => feedback.Reviewer)
               .SingleOrDefaultAsync(feedback => feedback.Id == id);

            if (auctionFeedback == null)
            {
                throw new ArgumentException("The feedback you are trying to get does not exist.");
            }

            AuctionFeedbackDto auctionFeedbackDto = auctionFeedback.ToDto();

            return auctionFeedbackDto;
        }

        public IQueryable<AuctionFeedbackDto> GetAllAuctionFeedbackBySellerId(string userId)
        {
            IQueryable<AuctionFeedback> auctionFeedback = auctionDbContext.AuctionFeedback;

            return auctionFeedback.Select(x=>x.ToDto(true,true));//.Where(feedback => feedback.Item.sellerUserId == userId).Select(feedback => feedback.ToDto(true,true));
        }

        public async Task<AuctionFeedbackDto> UpdateAuctionFeedback(long id, AuctionFeedbackDto auctionFeedbackDto)
        {
            AuctionFeedback auctionFeedback = await this.auctionDbContext
                .AuctionFeedback
                .SingleOrDefaultAsync(feedback => feedback.Id == id);

            if (auctionFeedback == null)
            {
                throw new ArgumentException("The feedback you are trying to update does not exist.");
            }

            auctionFeedback.FeedbackText = auctionFeedbackDto.FeedbackText;
            auctionFeedback.Rating = auctionFeedbackDto.Rating;

            this.auctionDbContext.Update(auctionFeedback);
            await this.auctionDbContext.SaveChangesAsync();

            return auctionFeedback.ToDto();
        }
    }
}
