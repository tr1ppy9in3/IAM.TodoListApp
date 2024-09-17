using IAD.TodoListApp.Chat.Core;
using Microsoft.AspNetCore.SignalR;

namespace IAD.TodoListApp.Chat.Service.Hubs;

public class ChatHub : Hub<IChatClient>
{
    public async Task SendMessage(string message)
    {
        var chatUser = new User(Context.User);

        var chatMessage = new Message { Content = message, Username = chatUser.Username };

        await Clients.All.ReceivedMessage(chatMessage, chatUser);

    }

}
