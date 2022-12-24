using ChatApp_SignalR_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ChatApp_SignalR_Api.Hubs;

public class ChatHub : Hub
{
    private readonly MessageService _messageService;

    public ChatHub(MessageService messageService)
    {
        _messageService = messageService;
    }

    [Authorize]
    public async Task SendMessage(string message)
    {
        var username = Context.User.FindFirstValue(ClaimTypes.Name);
        await Clients.All.SendAsync("GetMessage",username,message);
    }

    [Authorize]
    public async Task SendMessageToGroup(string group,string message)
    {
        var username = Context.User.FindFirstValue(ClaimTypes.Name);

        _messageService.Messages[group].Add(
            new Tuple<string, string>(username, message));

        await Clients.Groups(group)
            .SendAsync("GetMessage", username, message);
    }

    public async Task JoinGroup(string group)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId,group);
    }
}
