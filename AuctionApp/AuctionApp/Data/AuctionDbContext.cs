using AuctionApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Data
{
    public class AuctionAppDbContext : IdentityDbContext<AuctionUser, IdentityRole, string>
    {
        public DbSet<AuctionItemModel> AuctionItems { get; set; }
        public AuctionAppDbContext (DbContextOptions<AuctionAppDbContext> options) : base(options)
        {}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");

            base.OnConfiguring(optionsBuilder);
        }
     }
}
