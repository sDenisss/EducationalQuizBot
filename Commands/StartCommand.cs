using Telegram.Bot;
using Telegram.Bot.Types;

public class StartCommand : ICommand
{
    private readonly ITelegramBotClient _bot;
    private readonly IUserRepository _userRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IQuizRepository _quizRepository;
    private CancellationToken cancellationToken;

    public StartCommand(ITelegramBotClient bot, IUserRepository userRepository, IEducationRepository educationRepository, IQuizRepository quizRepository)
    {
        _bot = bot;
        _userRepository = userRepository;
        _educationRepository = educationRepository;
        _quizRepository = quizRepository;
    }

    public bool CanHandle(string messageText)
    {
        return messageText.Equals("/start", StringComparison.OrdinalIgnoreCase);
        
    }

    public async Task ExecuteAsync(Message message)
    {
        var chatId = message.Chat.Id;
        var userName = message.From?.Username ?? "гость";

        var response = $"Привет, {userName}! Я обучающий бот по C#.";

        // var scheduler = new RandomScheduler(botClient, educationRepository, quizRepository, chatId);

        //         scheduler.Start(update.Message, cts.Token);
        //         scheduler.StartQuiz(update.Message, cts.Token);

        //         // создаем новый поток
        //         Task.Run(() => scheduler.Start(update.Message, cts.Token));
        //         Task.Run(() => scheduler.StartQuiz(update.Message, cts.Token));


        var scheduler = new RandomScheduler(_bot, _educationRepository, _quizRepository, chatId);
        scheduler.StartMixed(message, CancellationToken.None);
        // scheduler.StartQuiz(message, CancellationToken.None);


        await _userRepository.SaveInDbasync(new User(userName), cancellationToken);
        await _bot.SendTextMessageAsync(chatId, response);
    }
}
