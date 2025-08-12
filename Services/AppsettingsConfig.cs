using EducationalQuizBot.Infrastructure;
using Microsoft.Extensions.Configuration;

public class AppsettingsConfig
{
    public static IConfigurationRoot Configuration()
    {
        Configs configs = new Configs();
        // Считывание конфигурации из файла
        var config = new ConfigurationBuilder()
            .AddJsonFile(configs.ConfigFile, optional: false)
            .Build();
        return config;
    }
}