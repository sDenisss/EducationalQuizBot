using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveInDbasync(User user, CancellationToken cancellationToken)
    {
        if (await _context.Users.FirstOrDefaultAsync(u => u.TelegramName == user.TelegramName) == null)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    
}