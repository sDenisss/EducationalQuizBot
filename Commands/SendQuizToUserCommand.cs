using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

public class SendQuizToUserCommand
{
    private readonly ITelegramBotClient _bot;
    private readonly IQuizRepository _quizRepository;
    private CancellationToken cancellationToken;

    public SendQuizToUserCommand(ITelegramBotClient bot, IQuizRepository quizRepository)
    {
        _bot = bot;
        _quizRepository = quizRepository;
    }

    public bool CanHandle(string messageText) => true;


    // public async Task ExecuteAsync(Message message)
    // {

    //     Task.Run(async () =>
    //     {
    //         while (!cancellationToken.IsCancellationRequested)
    //         {
    //             var delayMinutes = _random.Next(1, 3);
    //             await Task.Delay(TimeSpan.FromMinutes(delayMinutes), cancellationToken);

    //             var quiz = await _quizRepository.GetQuizFromDb(message, cancellationToken);
    //             if (quiz == null) return;

    //             // Текст вопроса
    //             var question = quiz.Question;

    //             var answerString = quiz.Answer;
    //             var answerIndex = 0;
    //             var ints = new List<int> { 0, 1, 2, 3 };

    //             // Убираем правильный ответ из списка индексов
    //             for (int i = 0; i < 4; i++)
    //             {
    //                 if (quiz.Answer == quiz.Choices[i])
    //                 {
    //                     answerIndex = i;
    //                     ints.Remove(i);
    //                     break; // правильный ответ только один
    //                 }
    //             }

    //             // Выбираем 3 случайных неправильных ответа
    //             var random = new Random();
    //             var incorrectOptions = ints.OrderBy(_ => random.Next()).Take(3).Select(index => quiz.Choices[index]).ToList();

    //             // Добавляем правильный ответ
    //             incorrectOptions.Add(quiz.Answer);

    //             // Перемешиваем все 4 варианта
    //             var options = incorrectOptions.OrderBy(_ => random.Next()).ToArray();


    //             // Перемешиваем ответы
    //             var rnd = new Random();
    //             var shuffledOptions = options.OrderBy(_ => rnd.Next()).ToArray();

    //             // Создаем инлайн-кнопки
    //             var buttons = shuffledOptions.Select((option, index) =>
    //                 InlineKeyboardButton.WithCallbackData(option,
    //                     option == quiz.Answer ? "correct" : "wrong")).ToArray();

    //             var keyboard = new InlineKeyboardMarkup(buttons.Chunk(2)); // 2x2 кнопки
                
    //             await _bot.SendPollAsync(
    //                     chatId: message.Chat.Id,
    //                     question: quiz.Question,
    //                     options: quiz.Choices,
    //                     type: PollType.Quiz,
    //                     correctOptionId: answerIndex, // индекс правильного ответа — "protected"
    //                     isAnonymous: true,
    //                     cancellationToken: cancellationToken
    //                 );


    //             // await _bot.SendTextMessageAsync(
    //             //     chatId: message.Chat.Id,
    //             //     text: question,
    //             //     replyMarkup: keyboard,
    //             //     cancellationToken: cancellationToken);
    //         }
    //     }, cancellationToken);

        //     var quiz = await _quizRepository.GetQuizFromDb(message, cancellationToken);
        //     if (quiz == null) return;

        //     // Текст вопроса
        //     var question = quiz.Question;

        //     var answerString = quiz.Answer;
        //     var ints = new List<int> { 0, 1, 2, 3 };

        //     // Убираем правильный ответ из списка индексов
        //     for (int i = 0; i < 4; i++)
        //     {
        //         if (quiz.Answer == quiz.Choices[i])
        //         {
        //             ints.Remove(i);
        //             break; // правильный ответ только один
        //         }
        //     }

        //     // Выбираем 3 случайных неправильных ответа
        //     var random = new Random();
        //     var incorrectOptions = ints.OrderBy(_ => random.Next()).Take(3).Select(index => quiz.Choices[index]).ToList();

        //     // Добавляем правильный ответ
        //     incorrectOptions.Add(quiz.Answer);

        //     // Перемешиваем все 4 варианта
        //     var options = incorrectOptions.OrderBy(_ => random.Next()).ToArray();


        //     // Перемешиваем ответы
        //     var rnd = new Random();
        //     var shuffledOptions = options.OrderBy(_ => rnd.Next()).ToArray();

        //     // Создаем инлайн-кнопки
        //     var buttons = shuffledOptions.Select((option, index) =>
        //         InlineKeyboardButton.WithCallbackData(option, 
        //             option == quiz.Answer ? "correct" : "wrong")).ToArray();

        //     var keyboard = new InlineKeyboardMarkup(buttons.Chunk(2)); // 2x2 кнопки

        //     await _bot.SendTextMessageAsync(
        //         chatId: message.Chat.Id,
        //         text: question,
        //         replyMarkup: keyboard,
        //         cancellationToken: cancellationToken);
        // }
    

}