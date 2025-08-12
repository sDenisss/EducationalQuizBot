using Telegram.Bot.Types;

public interface ICommand
{
    bool CanHandle(string messageText);
    Task ExecuteAsync(Message message);
}
