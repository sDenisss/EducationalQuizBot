using Telegram.Bot.Types;

public interface IEducationRepository
{
    Task SaveInDbasync(Education education, CancellationToken cancellationToken);
    // Task<Education?> GetRandomEducationFromDb(CancellationToken cancellationToken);
    Task<Education?> GetEducationFromDb(Message message, CancellationToken cancellationToken);
    // Task<Education?> GetRandomEducationAsync(CancellationToken cancellationToken);

}