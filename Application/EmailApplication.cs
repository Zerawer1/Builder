using EmailBuilderDemo.Builders;
using EmailBuilderDemo.Directors;
using EmailBuilderDemo.Models;

namespace EmailBuilderDemo.Application;

public sealed class DemoApplication
{
    private readonly EmailScenarioService _scenarioService;
    private readonly IEmailOutput _output;

    public DemoApplication()
        : this(new EmailScenarioService(), new ConsoleEmailOutput())
    {
    }

    public DemoApplication(EmailScenarioService scenarioService, IEmailOutput output)
    {
        _scenarioService = scenarioService;
        _output = output;
    }

    public void Run()
    {
        var isRunning = true;

        while (isRunning)
        {
            ShowMenu();

            var choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    _output.WriteEmail("Базовое письмо", _scenarioService.CreateBasicEmail());
                    break;
                case "2":
                    _output.WriteEmail("Письмо по шаблону", _scenarioService.CreateTemplateEmail());
                    break;
                case "3":
                    _output.WriteEmail("Письмо с вложением", _scenarioService.CreateAttachmentEmail());
                    break;
                case "0":
                    isRunning = false;
                    Console.WriteLine("Работа приложения завершена.");
                    break;
                default:
                    Console.WriteLine("Неизвестная команда. Выбери пункт меню от 0 до 3.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private static void ShowMenu()
    {
        Console.WriteLine("Демонстрация паттерна Builder");
        Console.WriteLine("1. Собрать базовое письмо");
        Console.WriteLine("2. Собрать письмо по готовому шаблону");
        Console.WriteLine("3. Собрать письмо с вложением");
        Console.WriteLine("0. Выход");
        Console.Write("Выбери пункт меню: ");
    }
}

public sealed class EmailScenarioService
{
    private readonly IEmailMessageBuilder _builder;
    private readonly EmailDirector _director;

    public EmailScenarioService()
        : this(new EmailMessageBuilder(), new EmailDirector())
    {
    }

    public EmailScenarioService(IEmailMessageBuilder builder, EmailDirector director)
    {
        _builder = builder;
        _director = director;
    }

    public EmailMessage CreateBasicEmail()
    {
        return _builder
            .Reset()
            .From("demo@example.com")
            .To("student@example.com")
            .Subject("Учебное уведомление")
            .TextBody("Это пример базового письма, собранного через билдер.")
            .Build();
    }

    public EmailMessage CreateTemplateEmail()
    {
        return _director.BuildTemplateEmail(
            _builder,
            from: "template@example.com",
            to: "student@example.com",
            topic: "Демонстрационный шаблон",
            text: "Это письмо показывает, как director может использовать готовый сценарий сборки.");
    }

    public EmailMessage CreateAttachmentEmail()
    {
        return _builder
            .Reset()
            .From("demo@example.com")
            .To("student@example.com")
            .Subject("Пример письма с вложением")
            .TextBody("К письму приложен демонстрационный файл.")
            .Attach("example.txt", "text/plain", new byte[] { 1, 2, 3, 4, 5 })
            .Header("X-Режим", "демо")
            .Build();
    }
}

public interface IEmailOutput
{
    void WriteEmail(string title, EmailMessage email);
}

public sealed class ConsoleEmailOutput : IEmailOutput
{
    public void WriteEmail(string title, EmailMessage email)
    {
        Console.WriteLine($"=== {title} ===");
        Console.WriteLine(email);
    }
}
