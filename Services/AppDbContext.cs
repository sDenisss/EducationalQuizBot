using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Education> Educations { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TelegramName).IsRequired();
            entity.Property(e => e.PointsAllTime);
            entity.Property(e => e.AnswersAnswered);
            entity.Property(e => e.RightAnswered);
            entity.Property(e => e.DeliveredEducations);
            entity.Property(e => e.DeliveredQuizzes);
            entity.Property(e => e.DateFirstStart).IsRequired();
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Question);
            entity.Property(e => e.Answer);
            entity.Property(e => e.Choices).HasColumnType("text[]");
            entity.Property(e => e.CreateDate);
        });

        modelBuilder.Entity<Education>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.EducationText);
            entity.Property(e => e.CreateDate);
        });

        modelBuilder.Entity<UserAnswer>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasOne<User>() // один пользователь
                    .WithMany()     // пока не делаем навигацию
                    .HasForeignKey(x => x.UserId);            
            entity.HasOne<Quiz>() 
                    .WithMany()
                    .HasForeignKey(x => x.QuizId);
        });
    }
}