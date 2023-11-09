using System;
using System.Diagnostics;
using System.IO;
using BarsGroupTestProject.Models;
using Newtonsoft.Json;

namespace BarsGroupTestProject;

public class UiTestBase
{
    private static AppSettings? _settings;
    private protected string? Url;
    private protected PerformanceGlitchUser? PerformanceGlitchUser;
    private protected WebDriver Driver = null!;
    
    [SetUp]
    public void Setup()
    {
        Driver = new ChromeDriver();
        Url = GetUrlFromPresetSettings();
        PerformanceGlitchUser = GetUserFromPresetSettings();
    }
    
    [TearDown]
    public void TearDown()
    {
        Driver.Close();
        Driver.Quit();
    }

    private static PerformanceGlitchUser? GetUserFromPresetSettings()
    {
        return GetDeserializeSettingJson().PerformanceGlitchUser;
    }

    private static string? GetUrlFromPresetSettings()
    {
        return GetDeserializeSettingJson().Url;
    }
    private static AppSettings GetDeserializeSettingJson()
    {
        if (_settings != null) return _settings;
        var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "appsettings.json");
        var json = File.ReadAllText(path);
        _settings = JsonConvert.DeserializeObject<AppSettings>(json);
        Debug.Assert(_settings != null, nameof(_settings) + " != null");
        return _settings;
    }

    public void StepNotificationInConsole(string stepName)
    {
        Console.WriteLine(stepName);
    }
}