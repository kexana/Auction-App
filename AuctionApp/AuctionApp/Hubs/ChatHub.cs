using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

using Microsoft.AspNetCore.Mvc;
using AuctionApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using AuctionApp.Services;
using AuctionApp.ModelDtos;
using Microsoft.AspNetCore.Identity;
using AuctionApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using AuctionApp.Areas.Users.Pages.Account;
using System.Security.Claims;
using AuctionApp.Services.Mapping;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private readonly UserManager<AuctionUser> userManager;

        public ChatHub(UserManager<AuctionUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task SendPrivateMessage(string sender, string recipient, string message)
        {
            await Clients.User(userManager.FindByNameAsync(recipient).Result.Id).PrivateMessage(sender, message);
        }
    }
}
