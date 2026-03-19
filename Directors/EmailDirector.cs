using EmailBuilderDemo.Builders;
using EmailBuilderDemo.Models;

namespace EmailBuilderDemo.Directors;

public sealed class EmailDirector
{
    public EmailMessage BuildTemplateEmail(IEmailMessageBuilder builder, string from, string to, string topic, string text)
    {
        return builder
            .Reset()
            .From(from)
            .To(to)
            .Subject(topic)
            .TextBody(text)
            .Header("X-Шаблон", "учебный")
            .Build();
    }

    public EmailMessage BuildHtmlTemplate(IEmailMessageBuilder builder, string from, string to, string topic, string html)
    {
        return builder
            .Reset()
            .From(from)
            .To(to)
            .Subject(topic)
            .HtmlBody(html)
            .Header("X-Формат", "html")
            .Build();
    }
}
