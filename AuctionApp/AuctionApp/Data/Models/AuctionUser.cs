using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace AuctionApp.Data.Models
{
    public class AuctionUser : IdentityUser
    {

        public ICollection<AuctionFeedback> Feedback { get; set; }

        [InverseProperty("Sender")]
        public virtual ICollection<Chat> SentChats { get; set; }
        [InverseProperty("Receiver")]
        public virtual ICollection<Chat> ReceivedChats { get; set; }
    }
}
