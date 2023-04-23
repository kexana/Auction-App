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
    public class AuctionPrivateMessageService : IAuctionPrivateMessageService
    {
        private AuctionAppDbContext auctionDbContext;

        private UserManager<AuctionUser> userManager;

        public AuctionPrivateMessageService(AuctionAppDbContext auctionDbContext, UserManager<AuctionUser> userManager)
        {
            this.auctionDbContext = auctionDbContext;
            this.userManager = userManager;
        }

        public async Task<AuctionPrivateMessageDto> CreateAuctionPrivateMessage(AuctionPrivateMessageDto auctionPrivateMessageDto, AuctionUser senderUser, AuctionUser recepiantUser)
        {
            AuctionPrivateMessage auctionPM = auctionPrivateMessageDto.ToEntity();

            auctionPM.Sender = senderUser;
            auctionPM.Recepiant = recepiantUser;
            auctionPM.PostedDate = DateTime.Now;
            auctionPM.Attachment = auctionPrivateMessageDto.Attachment;

            senderUser.SentMessages.Add(auctionPM);
            recepiantUser.RecievedMessages.Add(auctionPM);

            await this.auctionDbContext.AuctionPrivateMessages.AddAsync(auctionPM);
            await this.userManager.UpdateAsync(senderUser);
            await this.userManager.UpdateAsync(recepiantUser);
            await this.auctionDbContext.SaveChangesAsync();

            return auctionPM.ToDto();
        }

        public async Task<AuctionPrivateMessageDto> DeleteAuctionPrivateMessage(long id)
        {
            AuctionPrivateMessage auctionPM = await this.auctionDbContext
                .AuctionPrivateMessages
                .SingleOrDefaultAsync(pm => pm.Id == id);

            if (auctionPM == null)
            {
                throw new ArgumentException("The Private Message you are trying to delete does not exist.");
            }

            this.auctionDbContext.AuctionPrivateMessages.Remove(auctionPM);
            await this.auctionDbContext.SaveChangesAsync();

            return auctionPM.ToDto();
        }

        public IQueryable<AuctionPrivateMessageDto> GetAllAuctionPrivateMessages(bool fetchDeleted = false)
        {
            IQueryable<AuctionPrivateMessage> auctionPMs = auctionDbContext.AuctionPrivateMessages;

            return auctionPMs.Select(pm => pm.ToDto(true));
        }

        public IQueryable<AuctionPrivateMessageDto> GetAllAuctionPrivateMessagesByRecepiantId(string userId)
        {
            IQueryable<AuctionPrivateMessage> auctionPM = auctionDbContext.AuctionPrivateMessages;

            return auctionPM
                .Where(pm => pm.Recepiant.Id == userId)
                .Include(pm => pm.Recepiant)
                .Include(pm => pm.Sender)
                .Select(x => x.ToDto(true));
        }

        public IQueryable<AuctionPrivateMessageDto> GetAllAuctionPrivateMessagesBySenderId(string userId)
        {
            IQueryable<AuctionPrivateMessage> auctionPM = auctionDbContext.AuctionPrivateMessages;

            return auctionPM
                .Where(pm => pm.Sender.Id == userId)
                .Include(pm => pm.Recepiant)
                .Include(pm => pm.Sender)
                .Select(x => x.ToDto(true));
        }

        public IQueryable<AuctionPrivateMessageDto> GetAllAuctionPrivateMessagesBetweenSenderAndRecepiant(string senderId, string recepiantId)
        {
            IQueryable<AuctionPrivateMessage> auctionPM = auctionDbContext.AuctionPrivateMessages;

            return auctionPM
                .Where(pm => pm.Sender.Id == senderId || pm.Sender.Id == recepiantId)
                .Include(pm => pm.Recepiant)
                .Include(pm => pm.Sender)
                .OrderByDescending(pm => pm.PostedDate)
                .Select(x => x.ToDto(true));
        }

        public IQueryable<AuctionUserDto> GetAllAuctionUserWithCorrespondance(string senderId)
        {
            IQueryable<AuctionUserDto> auctionUsers = auctionDbContext.AuctionPrivateMessages.Where(pm => pm.SenderId == senderId).Select(pm => pm.Recepiant.ToDto(true,true));

            return auctionUsers;
        }

        public async Task<AuctionPrivateMessageDto> GetAuctionPrivateMessageById(long id)
        {
            AuctionPrivateMessage auctionPM = await this.auctionDbContext.AuctionPrivateMessages
               .Include(pm => pm.Sender)
               .Include(pm => pm.Recepiant)
               .SingleOrDefaultAsync(pm => pm.Id == id);

            if (auctionPM == null)
            {
                throw new ArgumentException("The Private Message you are trying to get does not exist.");
            }

            AuctionPrivateMessageDto auctionPMDto = auctionPM.ToDto();

            return auctionPMDto;
        }

        public async Task<AuctionPrivateMessageDto> UpdateAuctionPrivateMessage(long id, AuctionPrivateMessageDto auctionPrivateMessageDto)
        {
            AuctionPrivateMessage auctionPM = await this.auctionDbContext.AuctionPrivateMessages
                 .SingleOrDefaultAsync(pm => pm.Id == id);

            if (auctionPM == null)
            {
                throw new ArgumentException("The Private Message you are trying to update does not exist.");
            }

            auctionPM.Text = auctionPrivateMessageDto.Text;
            auctionPM.Attachment = auctionPrivateMessageDto.Attachment;

            this.auctionDbContext.Update(auctionPM);
            await this.auctionDbContext.SaveChangesAsync();

            return auctionPM.ToDto();
        }
    }
}
