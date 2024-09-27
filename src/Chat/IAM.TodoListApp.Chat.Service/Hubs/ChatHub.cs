using IAD.TodoListApp.Chat.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace IAD.TodoListApp.Chat.Service.Hubs;

[Authorize(Roles ="RegularUser")]
public class ChatHub : Hub<IChatClient>
{
    public async Task SendMessage(string message)
    {
        var chatUser = new User(Context.User);

        var chatMessage = new Message { Content = message, Username = chatUser.Username };

        await Clients.All.ReceivedMessage(chatMessage, chatUser);

    }

}
