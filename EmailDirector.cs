namespace BuilderEmailDemo;

public static class EmailDirector
{
    public static EmailMessage BuildPasswordReset(EmailMessageBuilder builder, string from, string to, string resetLink)
    {
        return builder
            .Reset()
            .From(from)
            .To(to)
            .Subject("Password reset")
            .TextBody($"Hi!\n\nUse this link to reset your password: {resetLink}\n\nIf you didn't request this, ignore this email.")
            .Header("X-Template", "password-reset")
            .Build();
    }

    public static EmailMessage BuildMarketing(EmailMessageBuilder builder, string from, string to, string html)
    {
        return builder
            .Reset()
            .From(from)
            .To(to)
            .Subject("Our new offers")
            .HtmlBody(html)
            .Header("List-Unsubscribe", "<mailto:unsubscribe@example.com>")
            .Build();
    }
}
