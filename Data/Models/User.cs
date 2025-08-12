public class User
{
    public Guid Id { get; private set; }
    public string? TelegramName { get; private set; }
    public double PointsAllTime { get; private set; }
    // public double PointsLastWeek { get; private set; }
    public int AnswersAnswered { get; private set; }
    public int RightAnswered { get; private set; }
    public int DeliveredEducations { get; set; }
    public int DeliveredQuizzes { get; set; }
    public DateTime DateFirstStart { get; private set; }
    private User(){ }
    public User(string? telegramName)
    {
        Id = Guid.NewGuid(); // Генерация нового уникального идентификатора
        TelegramName = telegramName;
        PointsAllTime = 0; // Изначально 0 очков
        AnswersAnswered = 0; // Изначально 0 ответов
        RightAnswered = 0; // Изначально 0 правильных ответов
        DeliveredEducations = 0;
        DeliveredQuizzes = 0;
        DateFirstStart = DateTime.UtcNow; // Установка даты начала на текущее время
    }
}