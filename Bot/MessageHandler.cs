using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EducationalQuizBot.Bot;

public class MessageHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly List<ICommand> _commands;
    private readonly IUserRepository _userRepository;
    private readonly IQuizRepository _quizRepository;
    private readonly IEducationRepository _educationRepository;
 
    public MessageHandler(ITelegramBotClient botClient, IUserRepository userRepository, IQuizRepository quizRepository, IEducationRepository educationRepository)
    {
        _botClient = botClient;

        if (userRepository == null)
        {
            throw new Exception("UserRepository is null");
        }
        if (quizRepository == null)
        {
            throw new Exception("QuizRepository is null");
        }
        if (educationRepository == null)
        {
            throw new Exception("EducationRepository is null");
        }

        _commands = new List<ICommand>{
            new StartCommand(botClient, userRepository, educationRepository, quizRepository),
            new StatsCommand(botClient),
            new LoadQuizToDbCommand(botClient, quizRepository),
            new LoadEducationToDbCommand(botClient, educationRepository),
            new HelpCommand(botClient),
            new DefaultCommand(botClient)
        };
    }

    public async Task HandleAsync(Message message)
    {
        if (message.Text is null) return;

        // var cts = new CancellationTokenSource();
        // var scheduler = new RandomScheduler(_botClient, educationRepository, quizRepository, chatId);

        // scheduler.Start(message, cts.Token);
        // scheduler.StartQuiz(message, cts.Token);

        foreach (var command in _commands)
        {
            if (command.CanHandle(message.Text))
            {
                await command.ExecuteAsync(message);
                return;
            }
        }
    }
}
