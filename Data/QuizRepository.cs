using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;

public class QuizRepository : IQuizRepository
{
    private readonly AppDbContext _context;

    public QuizRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Quiz?> GetQuizFromDb(Message message, CancellationToken cancellationToken)
    {
        var userName = message.From?.Username ?? "гость";

        var user = await _context.Users.FirstOrDefaultAsync(u => u.TelegramName == userName, cancellationToken);
        if (user == null)
            throw new Exception("user is null");

        // Получаем индекс следующего обучения (0-based -> 1-based)
        var nextQuizId = user.DeliveredQuizzes + 1;

        // Пытаемся найти обучение с таким Id
        var quiz = await _context.Quizzes
            .FirstOrDefaultAsync(e => e.Id == nextQuizId, cancellationToken);

        if (quiz != null)
        {
            user.DeliveredQuizzes++;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return quiz;
    }

    // public Task<Quiz?> GetRandomQuizFromDb(CancellationToken cancellationToken)
    // {
    //     var totalCount = await _context.Educations.CountAsync(cancellationToken);
    //     if (totalCount == 0)
    //         return null;

    //     var rand = new Random();
    //     int offset = rand.Next(totalCount);

    //     // return await _context.Quizzes
    //     //     .Skip(offset)
    //     //     .FirstOrDefaultAsync(cancellationToken);
    // }

    public async Task SaveInDbasync(Quiz quiz, CancellationToken cancellationToken)
    {
        
        _context.Quizzes.Add(quiz); 
        await _context.SaveChangesAsync(cancellationToken);
    }

    
}