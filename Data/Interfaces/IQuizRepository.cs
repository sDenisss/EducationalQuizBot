using Telegram.Bot.Types;

public interface IQuizRepository
{
    Task SaveInDbasync(Quiz quiz, CancellationToken cancellationToken);
    Task<Quiz?> GetQuizFromDb(Message message, CancellationToken cancellationToken);
}