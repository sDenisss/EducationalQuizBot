using Telegram.Bot;
using Telegram.Bot.Types;
public class DefaultCommand : ICommand
{
    private readonly ITelegramBotClient _bot;

    public DefaultCommand(ITelegramBotClient bot)
    {
        _bot = bot;
    }

    public bool CanHandle(string messageText) => true;

    public async Task ExecuteAsync(Message message)
    {
        await _bot.SendTextMessageAsync(message.Chat.Id, "Неизвестная команда. Попробуйте /help.");
    }
}