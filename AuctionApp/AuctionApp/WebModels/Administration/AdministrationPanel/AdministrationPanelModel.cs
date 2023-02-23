using AuctionApp.ModelDtos;

namespace AuctionApp.WebModels.Administration.AdministrationPanel
{
    public class AdministrationPanelModel
    {
        public List<AuctionItemDto> Items { get; set; }
        public List<AuctionUserDto> Users { get; set; }
    }
}
