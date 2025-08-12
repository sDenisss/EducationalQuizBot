using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Configuration;
using EducationalQuizBot.Infrastructure;
using EducationalQuizBot.Bot;
using Microsoft.EntityFrameworkCore;

public class Program
{
    // private readonly IUserRepository _userRepository;
    public static void Main(string[] args)
    {
        // var botClient = new TelegramBotClient("your-token");
        // var repo = new EducationRepository(appDbContext);
        // var chatId = 1234567890; // Заменить на нужный chatId

        // var scheduler = new RandomEducationScheduler(botClient, repo, chatId);
        var cts = new CancellationTokenSource();


        // // Запускаем "рандомную рассылку"
        // scheduler.Start(cts.Token);

        // Configs configs = new Configs();
        // // Считывание конфигурации из файла
        // var config = new ConfigurationBuilder()
        //     .AddJsonFile(configs.ConfigFile, optional: false)
        //     .Build();
        var config = AppsettingsConfig.Configuration();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        var dbContext = new AppDbContext(optionsBuilder.Options);
        var userRepository = new UserRepository(dbContext);
        var quizRepository = new QuizRepository(dbContext);
        var educationRepository = new EducationRepository(dbContext);
        

        // Достаём токен
        var botToken = config["TelegramBot:BotToken"];

        if (botToken == null)
        {
            throw new Exception("token is null");
        }

        var botClient = new TelegramBotClient(botToken);
        // запускаем поток myThread
        // myThread.Start();

        BotUpdateHandler messageHandler = new BotUpdateHandler(botClient, userRepository, quizRepository, educationRepository);

        botClient.StartReceiving(
            async (bot, update, token) =>
            {
                // var chatId = update.Message.Chat.Id;
                // var cts = new CancellationTokenSource();
                
                if (update.Message is { Text: { } messageText })
                {
                    var chatId = update.Message.Chat.Id;
                    var userName = update.Message.From?.Username ?? "Unknown";

                    await messageHandler.HandleUpdateAsync(update);
                }
            },
            async (bot, exception, token) =>
            {
                Console.WriteLine(exception.Message);
                await Task.CompletedTask;
            },
            new ReceiverOptions { AllowedUpdates = { } },
            cancellationToken: cts.Token
        );

        Console.WriteLine("Бот запущен. Enter для выхода.");
        Console.ReadLine();
        cts.Cancel();

    }
}
