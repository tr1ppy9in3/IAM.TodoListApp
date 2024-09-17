using IAM.TodoListApp.Chat.Core;

namespace IAM.TodoListApp.Chat.Service.Hubs;
public interface IChatClient
{
    Task ReceivedMessage(Message message, User user);
}
