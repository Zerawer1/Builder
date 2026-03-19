namespace BuilderApp;

public sealed class MarketingEmailDirector
{
    public EmailMessage CreatePromoEmail(IEmailMessageBuilder builder)
    {
        return builder
            .SetSender("sales@company.com")
            .SetRecipient("client@example.com")
            .SetSubject("Весенние скидки")
            .SetBody("<h1>Специальное предложение!</h1><p>Скидка 20% действует до пятницы.</p>", isHtml: true)
            .AddCc("manager@company.com")
            .AddAttachment("promo.pdf")
            .Build();
    }
}
