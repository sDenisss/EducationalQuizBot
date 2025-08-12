using Microsoft.EntityFrameworkCore;
using Npgsql;
using Telegram.Bot.Types;

public class EducationRepository : IEducationRepository
{
    private readonly AppDbContext _context;

    public EducationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveInDbasync(Education education, CancellationToken cancellationToken)
    {
        _context.Educations.Add(education);
        await _context.SaveChangesAsync(cancellationToken);
    }


    public async Task<Education?> GetEducationFromDb(Message message, CancellationToken cancellationToken)
    {
        var userName = message.From?.Username ?? "гость";

        var user = await _context.Users.FirstOrDefaultAsync(u => u.TelegramName == userName, cancellationToken);
        if (user == null)
            throw new Exception("user is null");

        // Получаем индекс следующего обучения (0-based -> 1-based)
        var nextEducationId = user.DeliveredEducations + 1;

        // Пытаемся найти обучение с таким Id
        var education = await _context.Educations
            .FirstOrDefaultAsync(e => e.Id == nextEducationId, cancellationToken);

        if (education != null)
        {
            user.DeliveredEducations++;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return education;
    }

    // public async Task<Education?> GetRandomEducationFromDb(CancellationToken cancellationToken)
    // {
    //     var random = await GetRandomEducationAsync(cancellationToken);
    //     return random;
    // }


    // public async Task<Education?> GetRandomEducationFromDb(CancellationToken cancellationToken)
    // {
    //     var totalCount = await _context.Educations.CountAsync(cancellationToken);
    //     if (totalCount == 0)
    //         return null;

    //     var rand = new Random();
    //     int offset = rand.Next(totalCount);

    //     return await _context.Educations
    //         .Skip(offset)
    //         .FirstOrDefaultAsync(cancellationToken);
    // }
}