namespace BuilderApp;

public sealed class EmailScenarioService
{
    private readonly MarketingEmailDirector _director;

    public EmailScenarioService(MarketingEmailDirector director)
    {
        _director = director;
    }

    public EmailMessage CreateDirectorEmail()
    {
        return _director.CreatePromoEmail(new EmailMessageBuilder());
    }

    public EmailMessage CreateCustomEmail()
    {
        return new EmailMessageBuilder()
            .SetSender("teacher@school.com")
            .SetRecipient("student@school.com")
            .SetSubject("Напоминание о домашнем задании")
            .SetBody("Пожалуйста, отправьте задание по паттерну Builder сегодня до 18:00.")
            .AddAttachment("builder-example.docx")
            .Build();
    }
}
