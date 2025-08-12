using System.Text;
using EducationalQuizBot.Infrastructure;

public static class FileWorker
{
    public static List<Quiz> FromFileToQuizList(string filePath)
    {
        var quizzes = new List<Quiz>();
        string? question = null;
        string? answer = null;
        string[] choices = Array.Empty<string>();

        foreach (var line in File.ReadLines(filePath))
        {
            if (line.StartsWith("Question:"))
                question = line["Question:".Length..].Trim();
            else if (line.StartsWith("Answer:"))
                answer = line["Answer:".Length..].Trim();
            else if (line.StartsWith("Choices:"))
                choices = line["Choices:".Length..].Split(',').Select(c => c.Trim()).ToArray();

            // Когда все три части получены — создаём квиз
            if (question != null && answer != null && choices.Length > 0)
            {
                quizzes.Add(new Quiz(question, answer, choices));
                question = null;
                answer = null;
                choices = Array.Empty<string>();
            }
        }
        return quizzes;
    }
    public static List<Education> FromFileToEduList(string filePath)
    {
        var educations = new List<Education>();
        var currentText = new StringBuilder();
        bool isEducationBlock = false;

        foreach (var line in File.ReadLines(filePath))
        {
            if (line.StartsWith("Edu:"))
            {
                // Если уже есть накопленный текст — сохраняем его
                if (currentText.Length > 0)
                {
                    educations.Add(new Education(currentText.ToString().Trim()));
                    currentText.Clear();
                }

                isEducationBlock = true;
                currentText.AppendLine(line["Edu:".Length..].Trim());
            }
            else if (line.StartsWith("Question:"))
            {
                // Заканчиваем текущий блок, если он был
                if (isEducationBlock && currentText.Length > 0)
                {
                    educations.Add(new Education(currentText.ToString().Trim()));
                    currentText.Clear();
                    isEducationBlock = false;
                }
            }
            else if (isEducationBlock)
            {
                currentText.AppendLine(line);
            }
        }

        // Добавляем последний блок, если файл не закончился на Question:
        if (isEducationBlock && currentText.Length > 0)
        {
            educations.Add(new Education(currentText.ToString().Trim()));
        }

        return educations;
    }

}
