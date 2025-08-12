using EducationalQuizBot.Infrastructure;
using Telegram.Bot;
using Telegram.Bot.Types;

public class LoadQuizToDbCommand : ICommand
{
    private readonly ITelegramBotClient _bot;
    private readonly IQuizRepository _quizRepository;
    private CancellationToken cancellationToken;

    public LoadQuizToDbCommand(ITelegramBotClient bot, IQuizRepository quizRepository)
    {
        _bot = bot;
        _quizRepository = quizRepository;
    }

    public bool CanHandle(string messageText)
    {
        return messageText.Equals("/loadQuizToDb", StringComparison.OrdinalIgnoreCase);
    }

    public async Task ExecuteAsync(Message message)
    {
        try
        {
            var config = AppsettingsConfig.Configuration();
            var configs = new Configs();
            var chatId = message.Chat.Id;
            var userName = message.From?.Username ?? "гость";
            var bossName = config["BossOfGym:TelegramName"];

            if (userName != bossName)
            {
                await _bot.SendTextMessageAsync(chatId, "У вас нет прав для этой команды.");
                return;
            }

            // var filePath = config["PathTo:fileForSaveQuizOrEdu"];
            var quizzes = FileWorker.FromFileToQuizList(configs.FileForQuizOrEdu);
            int count = 0;

            foreach (var quiz in quizzes)
            {
                await _quizRepository.SaveInDbasync(quiz, cancellationToken);
                count++;
            }

            await _bot.SendTextMessageAsync(chatId, $"✅ Загружено {count} квизов в базу данных.");
        }
        catch (System.Exception)
        {
            throw new Exception("Quiz error");
        }
    }

}
