using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

public class RandomScheduler
{
    private readonly ITelegramBotClient _bot;
    private readonly IEducationRepository _educationRepository;
    private readonly IQuizRepository _quizRepository;
    private readonly long _chatId;
    private readonly Random _random = new();

    public RandomScheduler(ITelegramBotClient bot, IEducationRepository educationRepository, IQuizRepository quizRepository, long chatId)
    {
        _bot = bot;
        _educationRepository = educationRepository;
        _quizRepository = quizRepository;
        _chatId = chatId;
    }
    
    public void StartMixed(Message message, CancellationToken cancellationToken)
    {
        try
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    // Случайная задержка между отправками
                    var delayMinutes = _random.Next(1, 2);
                    await Task.Delay(TimeSpan.FromMinutes(delayMinutes), cancellationToken);

                    // Случайно выбираем — отправить обучалку или квиз
                    bool sendQuiz = _random.Next(0, 2) == 0;

                    if (sendQuiz)
                    {
                        var quiz = await _quizRepository.GetQuizFromDb(message, cancellationToken);
                        if (quiz != null)
                        {
                            int answerIndex = Array.IndexOf(quiz.Choices, quiz.Answer);
                            await _bot.SendPollAsync(
                                chatId: message.Chat.Id,
                                question: quiz.Question,
                                options: quiz.Choices,
                                type: PollType.Quiz,
                                correctOptionId: answerIndex,
                                isAnonymous: true,
                                cancellationToken: cancellationToken
                            );
                        }
                    }
                    else
                    {
                        var edu = await _educationRepository.GetEducationFromDb(message, cancellationToken);
                        if (edu != null)
                        {
                            await _bot.SendTextMessageAsync(
                                chatId: _chatId,
                                text: edu.EducationText,
                                cancellationToken: cancellationToken
                            );
                        }
                    }
                }
            }, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка в смешанном старте", ex);
        }
    }


    // public void Start(Message message, CancellationToken cancellationToken)
    // {
    //     try
    //     {

    //         System.Console.WriteLine("2");
    //         Task.Run(async () =>
    //         {
    //             while (!cancellationToken.IsCancellationRequested)
    //             {
    //                 System.Console.WriteLine("3");
    //                 // ⏳ Случайная задержка (например, 1–10 минут)
    //                 var delayMinutes = _random.Next(1, 2);
    //                 await Task.Delay(TimeSpan.FromMinutes(delayMinutes), cancellationToken);

    //                 var edu = await _educationRepository.GetEducationFromDb(message, cancellationToken);

    //                 if (edu != null)
    //                 {
    //                     await _bot.SendTextMessageAsync(
    //                         chatId: _chatId,
    //                         text: edu.EducationText,
    //                         cancellationToken: cancellationToken);
    //                 }
    //             }
    //         }, cancellationToken);
    //     }
    //     catch (System.Exception)
    //     {
    //         throw new Exception("eee");
    //     }
    // }

    // public void StartQuiz(Message message, CancellationToken cancellationToken)
    // {
    //     try
    //     {
    //         System.Console.WriteLine("4");
    //         Task.Run(async () =>
    //         {

    //             while (!cancellationToken.IsCancellationRequested)
    //             {


    //                 System.Console.WriteLine("5");
    //                 var delayMinutes = _random.Next(1, 2);
    //                 await Task.Delay(TimeSpan.FromMinutes(delayMinutes), cancellationToken);

    //                 var quiz = await _quizRepository.GetQuizFromDb(message, cancellationToken);
    //                 if (quiz == null) return;

    //                 // Текст вопроса
    //                 var question = quiz.Question;

    //                 var answerString = quiz.Answer;
    //                 var answerIndex = 0;
    //                 var ints = new List<int> { 0, 1, 2, 3 };

    //                 // Убираем правильный ответ из списка индексов
    //                 for (int i = 0; i < 4; i++)
    //                 {
    //                     if (quiz.Answer == quiz.Choices[i])
    //                     {
    //                         answerIndex = i;
    //                         // ints.Remove(i);
    //                         break; // правильный ответ только один
    //                     }
    //                 }


    //                 if (quiz != null)
    //                 {
    //                     await _bot.SendPollAsync(
    //                                 chatId: message.Chat.Id,
    //                                 question: quiz.Question,
    //                                 options: quiz.Choices,
    //                                 type: PollType.Quiz,
    //                                 correctOptionId: answerIndex, // индекс правильного ответа — "protected"
    //                                 isAnonymous: true,
    //                                 cancellationToken: cancellationToken
    //                             );

    //                 }
    //             }
    //         }, cancellationToken);
    //     }
    //     catch (System.Exception)
    //     {
    //         throw new Exception("Error");
    //     }


    // }
}
