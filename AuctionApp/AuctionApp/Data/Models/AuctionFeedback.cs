using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AuctionApp.Data.Models
{
    public class AuctionFeedback
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public string FeedbackText { get; set; }

        [ForeignKey("Reviewer")]
        public string ReviewerId { get; set; }

        public AuctionUser Reviewer { get; set; }

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public AuctionItemModel Item { get; set; }
    }
}
