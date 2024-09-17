namespace IAD.TodoListApp.Chat.Core;

public class Message
{
    public required string Username { get; set; }
    public required string Content { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}
