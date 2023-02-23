using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AuctionApp.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public abstract class BaseUserController : Controller
    {
    }
}
