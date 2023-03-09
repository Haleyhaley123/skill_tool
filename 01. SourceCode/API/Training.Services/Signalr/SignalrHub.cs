using Microsoft.AspNetCore.SignalR;
using NTS.Common.Models;
using System.Threading.Tasks;

namespace Training.Services.Signalr
{
    public class SignalrHub : Hub<IHubClient>
    {
        public async Task DashBoard()
        {
            await Clients.All.DashBoard();
        }
    }
}