public class UserAnswer
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public int QuizId { get; private set; }
    public bool IsCorrect { get; private set; }
    public double PointsEarned { get; private set; }
    public DateTime AnsweredAt { get; private set; }

    // EF constructor
    private UserAnswer() { }

    public UserAnswer(Guid userId, int quizId, bool isCorrect, double pointsEarned)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        QuizId = quizId;
        IsCorrect = isCorrect;
        PointsEarned = pointsEarned;
        AnsweredAt = DateTime.UtcNow;
    }
}
