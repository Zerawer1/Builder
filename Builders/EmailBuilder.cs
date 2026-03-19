using EmailBuilderDemo.Models;

namespace EmailBuilderDemo.Builders;

public interface IEmailMessageBuilder
{
    IEmailMessageBuilder From(string from);
    IEmailMessageBuilder To(params string[] recipients);
    IEmailMessageBuilder Cc(params string[] recipients);
    IEmailMessageBuilder Bcc(params string[] recipients);
    IEmailMessageBuilder Subject(string subject);
    IEmailMessageBuilder TextBody(string text);
    IEmailMessageBuilder HtmlBody(string html);
    IEmailMessageBuilder Header(string name, string value);
    IEmailMessageBuilder Attach(string fileName, string contentType, byte[] bytes);
    IEmailMessageBuilder Reset();
    EmailMessage Build();
}

public sealed class EmailMessageBuilder : IEmailMessageBuilder
{
    private string? _from;
    private readonly List<string> _to = new();
    private readonly List<string> _cc = new();
    private readonly List<string> _bcc = new();

    private string? _subject;
    private string? _textBody;
    private string? _htmlBody;

    private readonly Dictionary<string, string> _headers = new(StringComparer.OrdinalIgnoreCase);
    private readonly List<EmailAttachment> _attachments = new();

    public IEmailMessageBuilder From(string from)
    {
        _from = RequireNotBlank(from, nameof(from));
        return this;
    }

    public IEmailMessageBuilder To(params string[] recipients)
    {
        AddMany(_to, recipients, nameof(recipients));
        return this;
    }

    public IEmailMessageBuilder Cc(params string[] recipients)
    {
        AddMany(_cc, recipients, nameof(recipients));
        return this;
    }

    public IEmailMessageBuilder Bcc(params string[] recipients)
    {
        AddMany(_bcc, recipients, nameof(recipients));
        return this;
    }

    public IEmailMessageBuilder Subject(string subject)
    {
        _subject = RequireNotBlank(subject, nameof(subject));
        return this;
    }

    public IEmailMessageBuilder TextBody(string text)
    {
        _textBody = text;
        return this;
    }

    public IEmailMessageBuilder HtmlBody(string html)
    {
        _htmlBody = html;
        return this;
    }

    public IEmailMessageBuilder Header(string name, string value)
    {
        _headers[RequireNotBlank(name, nameof(name))] = value;
        return this;
    }

    public IEmailMessageBuilder Attach(string fileName, string contentType, byte[] bytes)
    {
        fileName = RequireNotBlank(fileName, nameof(fileName));
        contentType = RequireNotBlank(contentType, nameof(contentType));
        bytes = bytes ?? throw new ArgumentNullException(nameof(bytes));

        _attachments.Add(new EmailAttachment(fileName, contentType, bytes));
        return this;
    }

    public EmailMessage Build()
    {
        if (string.IsNullOrWhiteSpace(_from))
            throw new InvalidOperationException("From is required.");
        if (_to.Count == 0)
            throw new InvalidOperationException("At least one To recipient is required.");
        if (string.IsNullOrWhiteSpace(_subject))
            throw new InvalidOperationException("Subject is required.");
        if (_textBody is null && _htmlBody is null)
            throw new InvalidOperationException("Either TextBody or HtmlBody must be set.");

        return new EmailMessage
        {
            From = _from,
            To = _to.ToArray(),
            Cc = _cc.ToArray(),
            Bcc = _bcc.ToArray(),
            Subject = _subject,
            TextBody = _textBody,
            HtmlBody = _htmlBody,
            Headers = new Dictionary<string, string>(_headers, StringComparer.OrdinalIgnoreCase),
            Attachments = _attachments.ToArray()
        };
    }

    public IEmailMessageBuilder Reset()
    {
        _from = null;
        _to.Clear();
        _cc.Clear();
        _bcc.Clear();
        _subject = null;
        _textBody = null;
        _htmlBody = null;
        _headers.Clear();
        _attachments.Clear();
        return this;
    }

    private static void AddMany(List<string> target, IEnumerable<string> values, string paramName)
    {
        if (values is null) throw new ArgumentNullException(paramName);
        foreach (var value in values)
            target.Add(RequireNotBlank(value, paramName));
    }

    private static string RequireNotBlank(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be empty.", paramName);
        return value;
    }
}
