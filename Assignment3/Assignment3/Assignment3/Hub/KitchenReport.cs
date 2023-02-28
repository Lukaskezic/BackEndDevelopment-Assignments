using Microsoft.AspNetCore.SignalR;
namespace Assignment3.Hub
{
    public class KitchenReport : Hub<IKitchenReport>
    {
        public async Task KitchenUpdate()
        {
            await Clients.All.KitchenUpdate();
        }
    }
}
