public class Education
{
    // public Guid Id { get; private set; }
    [System.ComponentModel.DataAnnotations.Key]
    public int Id { get; set; }
    public string? EducationText { get; set; }
    public DateTime CreateDate { get; private set; }
    // DbWorker dbWorker = new DbWorker();
    // private static int nextId = dbWorker.CountOfEduInDbAsync + 1;

    // Приватный конструктор для использования только в других конструкторах или методах.
    private Education() { }

    // Конструктор для создания экземпляра класса с текстом образования
    // public Education(string? educationText)
    // {
    //     Id = Guid.NewGuid(); // Генерация нового уникального идентификатора
    //     EducationText = educationText;
    //     CreateDate = DateTime.UtcNow; // Установка даты создания на текущее время
    // }
    public Education(string? educationText)
    {
        // Id = Guid.NewGuid(); // Генерация нового уникального идентификатора
        // Id = nextId++;
        EducationText = educationText;
        CreateDate = DateTime.UtcNow; // Установка даты создания на текущее время
    }

    
}