namespace EducationalQuizBot.Infrastructure;

public class Configs
{
    public string? ConfigFile { get; }
    public string? FileForQuizOrEdu { get; }
    

    public Configs()
    {
        ConfigFile = GetProjectRootPath("appsettings.json");
        FileForQuizOrEdu = GetProjectRootPath("fileForSaveQuizOrEdu.txt");
    }

    private static string GetProjectRootPath(string fileName)
    {
        string binPath = AppContext.BaseDirectory;
        string projectRoot = binPath[..binPath.IndexOf("bin")];
        return Path.Combine(projectRoot, fileName);
    }
    

}
