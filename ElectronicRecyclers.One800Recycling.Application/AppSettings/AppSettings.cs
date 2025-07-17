using Microsoft.Extensions.Configuration;
using System.IO;

public static class AppSettings
{
    public static IConfiguration Configuration { get; }

    static AppSettings()
    {
        Configuration = new ConfigurationManager()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public static string Get(string key) => Configuration[key];
}