using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using notifier.Hub;

namespace notifier.Controllers;

public class ExprController : Controller
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public ExprController(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task<IActionResult> Order()
    {
        var message = "New Order arrived at " + DateTime.Now.ToString("G");
        await _hubContext.Clients.All.SendAsync("onOrder", message);
        return Ok(new
        {
            message
        });
    }
}