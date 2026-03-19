namespace BuilderEmailDemo;

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
            : string.Join(", ", Attachments.Select(a => $"{a.FileName} ({a.ContentType}, {a.Bytes.Length} bytes)"));

        return $"From: {From}\n" +
               $"To: {Join(To)}\n" +
               $"Cc: {Join(Cc)}\n" +
               $"Bcc: {Join(Bcc)}\n" +
               $"Subject: {Subject}\n" +
               $"Headers: {headers}\n" +
               $"Attachments: {attachments}\n" +
               $"TextBody: {(TextBody is null ? "(null)" : "<set>")}\n" +
               $"HtmlBody: {(HtmlBody is null ? "(null)" : "<set>")}\n";
    }
}

public sealed record EmailAttachment(string FileName, string ContentType, byte[] Bytes);
