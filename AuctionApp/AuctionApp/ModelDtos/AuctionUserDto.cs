namespace AuctionApp.ModelDtos
{
    public class AuctionUserDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public ICollection<AuctionFeedbackDto> Feedback { get; set; }

    }
}
