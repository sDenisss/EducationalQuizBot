# 📚 Educational Quiz Telegram Bot

This is a **C# Telegram Bot** built with `.NET` that allows users to register, participate in quizzes, earn points, and view statistics.  
The bot uses **PostgreSQL** for storing data and supports adding your own educational content.

---

## 🚀 Features
- User registration with Telegram
- Multiple-choice quizzes (one correct + random incorrect answers)
- Point system(soon)
- Statistics tracking(soon)
- Easy to add new questions via database
- Admin commands for managing content

---

## 🛠 Requirements
Before running the bot, make sure you have:
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- A [Telegram bot token](https://core.telegram.org/bots#how-do-i-create-a-bot) from **BotFather**

---

## 📂 Project Configuration
The bot uses an `appsettings.json` file for configuration.

### Example `appsettings.json`:
```json
{
    "TelegramBot": {
        "BotToken": "YOUR_TELEGRAM_BOT_TOKEN"
    },
    "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Port=5432;Database=EducationalQuizBot;Username=YOUR_DB_USER;Password=YOUR_DB_PASSWORD"
    },
    "BossOfGym": {
        "TelegramName": "YOUR_TELEGRAM_USERNAME"
    }
}
```

What to change:
TelegramBot.BotToken → Replace with your bot token from BotFather.
ConnectionStrings.DefaultConnection → Update DB name, user, and password to match your PostgreSQL setup.
BossOfGym.TelegramName → Your Telegram username (for admin rights in the bot).

📦 Database Setup
Install PostgreSQL and create a new database
Update your appsettings.json with the correct credentials.
Run EF Core migrations (if the project uses migrations):

```bash
dotnet ef database update
```

Add quiz questions into the database table (usually named Questions or similar).

▶️ Running the Bot
Restore dependencies:

```bash
dotnet restore
```

Build the project:

```bash
dotnet build
```

Run the bot:

```bash
dotnet run
```

📚 Adding Content to the Quiz
To add new quiz:

Example text in fileForSaveQuizOrEdu.txt:
```bash 
Question: What is the HTTP method typically used for creating a new resource in ASP.NET Core?
Answer: POST
Choices: GET, PUT, DELETE, POST
```

and /loadQuizToDb in tg chat

📚 Adding Content to the Education
To add new education:

Example text in fileForSaveQuizOrEdu.txt:
```bash 
Edu: Ключевое слово static в C# указывает, что член класса принадлежит самому классу, а не его экземпляру.
Статические члены можно вызывать без создания объекта:
Console.WriteLine(Math.PI);
```
and /loadEducationToDb in tg chat

--- 

📄 License
MIT License — free to use and modify.