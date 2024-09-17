using IAD.TodoListApp.Chat.Core;

namespace IAD.TodoListApp.Chat.Service.Hubs;
public interface IChatClient
{
    Task ReceivedMessage(Message message, User user);
}
