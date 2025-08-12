using Telegram.Bot;
using Telegram.Bot.Types;

public class SendEduToUserCommand
{
    private readonly ITelegramBotClient _bot;
    private readonly IEducationRepository _educationRepository;
    private CancellationToken cancellationToken;

    public SendEduToUserCommand(ITelegramBotClient bot, IEducationRepository educationRepository)
    {
        _bot = bot;
        _educationRepository = educationRepository;
    }

    public bool CanHandle(string messageText) => true;

    // public async Task ExecuteAsync(Message message)
    // {
    //     // var randomEducation = await _educationRepository.GetRandomEducationFromDb(cancellationToken);
    //     var education = await _educationRepository.GetEducationFromDb(message, cancellationToken);

    //     if (education != null)
    //     {
    //         await _bot.SendTextMessageAsync(
    //             chatId: message.Chat.Id,
    //             text: education.EducationText,
    //             cancellationToken: cancellationToken);
    //     }
    // }
}