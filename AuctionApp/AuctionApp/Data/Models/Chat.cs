
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace AuctionApp.Data.Models
{
    public class Chat
    {
        public int ChatId { get; set; }

        public int SenderId { get; set; }
        public virtual AuctionUser Sender { get; set; }

        public int ReceiverId { get; set; }
        public virtual AuctionUser Receiver { get; set; }

        public virtual List<AuctionMessage> Messages { get; set; }
    }
}
