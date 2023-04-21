namespace AuctionApp.Hubs
{
    public interface IChatClient
    {
        Task PrivateMessage(string sender, string message);
    }
}
