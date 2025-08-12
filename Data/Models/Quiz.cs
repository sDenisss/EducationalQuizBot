public class Quiz
{
    // public Guid Id { get; private set; }
    [System.ComponentModel.DataAnnotations.Key]
    public int Id { get; private set; }
    public string? Question { get; private set; }
    public string[] Choices { get; private set; } = Array.Empty<string>();
    // public virtual ICollection<string?> Choices { get; private set; } = new List<string?>();
    public string? Answer { get; private set; }
    public DateTime CreateDate { get; private set; }

    // private static int nextId = 1;

    // Приватный конструктор для использования только в других конструкторах или методах.
    private Quiz() { }

    // Конструктор для создания экземпляра класса с вопросом и ответом
    public Quiz(string? question, string? answer, string[] choices)
    {
        // Id = Guid.NewGuid(); // Генерация нового уникального идентификатора
        // Id = nextId++;
        Question = question;
        Answer = answer;
        Choices = choices.ToArray(); // Инициализация списка вариантов ответов
        CreateDate = DateTime.UtcNow; // Установка даты создания на текущее время
    }
}