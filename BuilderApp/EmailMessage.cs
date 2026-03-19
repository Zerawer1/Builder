using System.Text;

namespace BuilderApp;

public sealed class EmailMessage
{
    public string From { get; }
    public string To { get; }
    public string Subject { get; }
    public string Body { get; }
    public bool IsHtml { get; }
    public IReadOnlyList<string> Cc { get; }
    public IReadOnlyList<string> Attachments { get; }

    public EmailMessage(
        string from,
        string to,
        string subject,
        string body,
        bool isHtml,
        IReadOnlyList<string> cc,
        IReadOnlyList<string> attachments)
    {
        From = from;
        To = to;
        Subject = subject;
        Body = body;
        IsHtml = isHtml;
        Cc = cc;
        Attachments = attachments;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine("=== Электронное письмо ===");
        builder.AppendLine($"От кого: {From}");
        builder.AppendLine($"Кому: {To}");
        builder.AppendLine($"Тема: {Subject}");
        builder.AppendLine($"Формат: {(IsHtml ? "HTML" : "Обычный текст")}");
        builder.AppendLine($"Копия: {(Cc.Count > 0 ? string.Join(", ", Cc) : "нет")}");
        builder.AppendLine($"Вложения: {(Attachments.Count > 0 ? string.Join(", ", Attachments) : "нет")}");
        builder.AppendLine("Содержимое:");
        builder.AppendLine(Body);
        return builder.ToString();
    }
}
