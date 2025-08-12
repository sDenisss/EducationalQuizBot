using Microsoft.EntityFrameworkCore;

public class DbWorker
{
    private readonly AppDbContext _context;

    public DbWorker(AppDbContext context)
    {
        _context = context;
    }

    // Получить количество строк в таблице Education
    public async Task<int> CountOfEduInDbAsync()
    {
        return await _context.Educations.CountAsync();
    }
}
