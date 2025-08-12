public interface IUserRepository
{
    Task SaveInDbasync(User user, CancellationToken cancellationToken);
}