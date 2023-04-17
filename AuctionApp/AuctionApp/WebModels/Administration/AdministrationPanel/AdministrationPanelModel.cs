using AuctionApp.ModelDtos;

namespace AuctionApp.WebModels.Administration.AdministrationPanel
{
    public class AdministrationPanelModel
    {
        public List<AuctionItemDto> Items { get; set; }
        public List<AuctionUserDto> Users { get; set; }
        public List<AuctionBidDto> Bids { get; set; }
        public List<AuctionFeedbackDto> Feedback { get; set; }
    }
}
