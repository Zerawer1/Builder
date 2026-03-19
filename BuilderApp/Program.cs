namespace BuilderApp;

internal static class Program
{
    private static void Main()
    {
        var scenarioService = new EmailScenarioService(new MarketingEmailDirector());
        var renderer = new EmailConsoleRenderer();

        var directorEmail = scenarioService.CreateDirectorEmail();
        var customEmail = scenarioService.CreateCustomEmail();

        renderer.ShowDemo(directorEmail, customEmail);
    }
}
