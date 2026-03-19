namespace BuilderApp;

public sealed class EmailMessageBuilder : IEmailMessageBuilder
{
    private string _sender = string.Empty;
    private string _recipient = string.Empty;
    private string _subject = "(без темы)";
    private string _body = string.Empty;
    private bool _isHtml;
    private readonly List<string> _cc = new();
    private readonly List<string> _attachments = new();

    public IEmailMessageBuilder SetSender(string sender)
    {
        _sender = sender;
        return this;
    }

    public IEmailMessageBuilder SetRecipient(string recipient)
    {
        _recipient = recipient;
        return this;
    }

    public IEmailMessageBuilder SetSubject(string subject)
    {
        _subject = subject;
        return this;
    }

    public IEmailMessageBuilder SetBody(string body, bool isHtml = false)
    {
        _body = body;
        _isHtml = isHtml;
        return this;
    }

    public IEmailMessageBuilder AddCc(string cc)
    {
        _cc.Add(cc);
        return this;
    }

    public IEmailMessageBuilder AddAttachment(string attachment)
    {
        _attachments.Add(attachment);
        return this;
    }

    public EmailMessage Build()
    {
        if (string.IsNullOrWhiteSpace(_sender))
        {
            throw new InvalidOperationException("Не указан адрес отправителя.");
        }

        if (string.IsNullOrWhiteSpace(_recipient))
        {
            throw new InvalidOperationException("Не указан адрес получателя.");
        }

        return new EmailMessage(
            _sender,
            _recipient,
            _subject,
            _body,
            _isHtml,
            _cc.AsReadOnly(),
            _attachments.AsReadOnly());
    }
}
