using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EducationalQuizBot.Bot;


public class BotUpdateHandler
{
    private readonly MessageHandler _messageHandler;
    // private readonly IUserRepository _userRepository;

    public BotUpdateHandler(ITelegramBotClient botClient,
                            IUserRepository userRepository,
                            IQuizRepository quizRepository,
                            IEducationRepository educationRepository)
    {
        _messageHandler = new MessageHandler(botClient, userRepository, quizRepository, educationRepository);
    }

    public async Task HandleUpdateAsync(Update update)
    {
        if (update.Type == UpdateType.Message && update.Message is not null)
        {
            await _messageHandler.HandleAsync(update.Message);
        }
    }
}