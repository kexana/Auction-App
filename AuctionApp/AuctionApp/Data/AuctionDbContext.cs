using AuctionApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Data
{
    public class AuctionAppDbContext : IdentityDbContext<AuctionUser, IdentityRole, string>
    {
        public DbSet<AuctionItemModel> AuctionItems { get; set; }
        public DbSet<AuctionBid> AuctionBids { get; set; }
        public DbSet<AuctionFeedback> AuctionFeedback { get; set; }
        public DbSet<AuctionPrivateMessage> AuctionPrivateMessages { get; set; }
        public AuctionAppDbContext (DbContextOptions<AuctionAppDbContext> options) : base(options)
        {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=auction_app_db;Trusted_Connection=True;MultipleActiveResultSets=true");

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuctionItemModel>()
                .HasMany(a => a.Bids)
                .WithOne(b => b.Item)
                .HasForeignKey(k => k.ItemId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<AuctionUser>()
                .HasMany(a => a.Feedback)
                .WithOne(b => b.Reviewer)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AuctionUser>()
                .HasMany(a => a.SentMessages)
                .WithOne(b => b.Sender)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AuctionUser>()
                .HasMany(a => a.RecievedMessages)
                .WithOne(b => b.Recepiant)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AuctionPrivateMessage>()
                .HasOne(a => a.Sender)
                .WithMany(b => b.SentMessages)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AuctionPrivateMessage>()
                .HasOne(a => a.Recepiant)
                .WithMany(b => b.RecievedMessages)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
