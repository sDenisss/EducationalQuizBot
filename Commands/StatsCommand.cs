using Telegram.Bot;
using Telegram.Bot.Types;

public class StatsCommand : ICommand
{
    private readonly ITelegramBotClient _bot;
    

    public StatsCommand(ITelegramBotClient bot)
    {
        _bot = bot;
    }

    public bool CanHandle(string messageText)
    {
        return messageText.Equals("/stats", StringComparison.OrdinalIgnoreCase);
    }

    public async Task ExecuteAsync(Message message)
    {
        var chatId = message.Chat.Id;
        var userName = message.From?.Username ?? "гость";

        // var points = ;
        // var response = $"У тебя {points} очков";
        // await _bot.SendTextMessageAsync(chatId, response);
    }
}
