namespace BuilderApp;

public interface IEmailMessageBuilder
{
    IEmailMessageBuilder SetSender(string sender);
    IEmailMessageBuilder SetRecipient(string recipient);
    IEmailMessageBuilder SetSubject(string subject);
    IEmailMessageBuilder SetBody(string body, bool isHtml = false);
    IEmailMessageBuilder AddCc(string cc);
    IEmailMessageBuilder AddAttachment(string attachment);
    EmailMessage Build();
}
