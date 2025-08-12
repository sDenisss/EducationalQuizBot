using Telegram.Bot;
using Telegram.Bot.Types;

public class HelpCommand : ICommand
{
    private readonly ITelegramBotClient _bot;
    public HelpCommand(ITelegramBotClient bot)
    {
        _bot = bot;
    }
    public bool CanHandle(string messageText)
    {
        return messageText.Equals("/help", StringComparison.OrdinalIgnoreCase);
    }

    public async Task ExecuteAsync(Message message)
    {
        var chatId = message.Chat.Id;

        var helpText = "📌 *Список доступных команд:*\n\n" +
                    "/start — Запустить бота и зарегистрироваться\n" +
                    "/stats — Показать вашу статистику\n" +
                    "/loadQuizToDb — Загрузить вопросы викторины в БД\n" +
                    "/loadEducationToDb — Загрузить обучающие материалы в БД\n" +
                    "/help — Показать это сообщение\n" +
                    "(любое другое сообщение) — Бот ответит, что команда не найдена";

        await _bot.SendTextMessageAsync(
            chatId, 
            helpText, 
            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
        );
    }

}