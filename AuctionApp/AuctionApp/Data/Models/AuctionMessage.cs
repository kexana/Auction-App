
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AuctionApp.Data.Models
{
    public class AuctionMessage
    {
        [Key]
        public int MessageId { get; set; }

        [ForeignKey("Chat")]
        public int ChatId { get; set; }

        [Required, Display(Name = "Message")]
        public string TextMessage { get; set; }

        [Required, Display(Name = "Time")]
        public DateTime MessageSent { get; set; }

        public virtual Chat Chat { get; set; }

        public Boolean SenderIsReceiver { get; set; }
    }
}
