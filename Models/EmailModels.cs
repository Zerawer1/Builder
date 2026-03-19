namespace EmailBuilderDemo.Models;

public sealed class EmailMessage
{
    public required string From { get; init; }
    public required IReadOnlyList<string> To { get; init; }
    public IReadOnlyList<string> Cc { get; init; } = Array.Empty<string>();
    public IReadOnlyList<string> Bcc { get; init; } = Array.Empty<string>();

    public required string Subject { get; init; }
    public string? TextBody { get; init; }
    public string? HtmlBody { get; init; }

    public IReadOnlyDictionary<string, string> Headers { get; init; } = new Dictionary<string, string>();
    public IReadOnlyList<EmailAttachment> Attachments { get; init; } = Array.Empty<EmailAttachment>();

    public override string ToString()
    {
        static string Join(IEnumerable<string> items) => string.Join(", ", items);

        var headers = Headers.Count == 0
            ? "(none)"
            : string.Join("; ", Headers.Select(kv => $"{kv.Key}={kv.Value}"));

        var attachments = Attachments.Count == 0
            ? "(none)"
            : string.Join(", ", Attachments.Select(item => $"{item.FileName} ({item.ContentType}, {item.Bytes.Length} bytes)"));

        return $"Отправитель: {From}\n" +
               $"Получатели: {Join(To)}\n" +
               $"Копия: {Join(Cc)}\n" +
               $"Скрытая копия: {Join(Bcc)}\n" +
               $"Тема: {Subject}\n" +
               $"Заголовки: {headers}\n" +
               $"Вложения: {attachments}\n" +
               $"Текстовое тело: {(TextBody is null ? "(не задано)" : "<задано>")}\n" +
               $"HTML-тело: {(HtmlBody is null ? "(не задано)" : "<задано>")}\n";
    }
}

public sealed record EmailAttachment(string FileName, string ContentType, byte[] Bytes);
