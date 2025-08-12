using EducationalQuizBot.Infrastructure;
using Telegram.Bot;
using Telegram.Bot.Types;

public class LoadEducationToDbCommand : ICommand
{
    private readonly ITelegramBotClient _bot;
    private readonly IEducationRepository _educationRepository;
    private CancellationToken cancellationToken;

    public LoadEducationToDbCommand(ITelegramBotClient bot, IEducationRepository educationRepository)
    {
        _bot = bot;
        _educationRepository = educationRepository;
    }

    public bool CanHandle(string messageText)
    {
        return messageText.Equals("/loadEducationToDb", StringComparison.OrdinalIgnoreCase);
    }

    public async Task ExecuteAsync(Message message)
    {
        try{
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
            var educations = FileWorker.FromFileToEduList(configs.FileForQuizOrEdu);
            int count = 0;

            foreach (var edu in educations)
            {
                await _educationRepository.SaveInDbasync(edu, cancellationToken);
                count++;
            }

            await _bot.SendTextMessageAsync(chatId, $"✅ Загружено {count} обучений в базу данных.");
        }
        catch (Exception ex)
        {
            await _bot.SendTextMessageAsync(message.Chat.Id, $"❌ Ошибка: {ex.Message}\n{ex.InnerException?.Message}");
            throw;
        }
    }

}
