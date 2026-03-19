using BuilderEmailDemo;

var builder = new EmailMessageBuilder();

var simple = builder
    .From("noreply@example.com")
    .To("user1@example.com")
    .Subject("Welcome")
    .TextBody("Hello! Your account has been created.")
    .Build();

Console.WriteLine("=== Simple email ===");
Console.WriteLine(simple);

var reset = EmailDirector.BuildPasswordReset(
    builder,
    from: "security@example.com",
    to: "user2@example.com",
    resetLink: "https://example.com/reset?token=abc123");

Console.WriteLine("=== Director-built email (password reset) ===");
Console.WriteLine(reset);

var withAttachments = builder
    .Reset()
    .From("billing@example.com")
    .To("user3@example.com")
    .Cc("accounting@example.com")
    .Subject("Invoice")
    .TextBody("Invoice is attached.")
    .Attach("invoice.pdf", "application/pdf", new byte[] { 1, 2, 3, 4, 5 })
    .Header("X-Priority", "1")
    .Build();

Console.WriteLine("=== Email with attachment ===");
Console.WriteLine(withAttachments);
