using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Processes
{
    public class ProgressBarHub : Hub
    {
        public async Task SendProgress(double percent)
        {
            await Clients.All.SendAsync("broadcastProgress", percent);
        }

        public async Task SendError(string error)
        {
            await Clients.All.SendAsync("broadcastError", error);
        }
    }
}
