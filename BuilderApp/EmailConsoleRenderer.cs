using System.Text;

namespace BuilderApp;

public sealed class EmailConsoleRenderer
{
    public void ShowDemo(EmailMessage directorEmail, EmailMessage customEmail)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("Демонстрация паттерна «Строитель» на примере конструктора email-сообщения.\n");
        Console.WriteLine("1. Письмо, созданное через директора:");
        Console.WriteLine(directorEmail);
        Console.WriteLine("2. Письмо, созданное вручную через строителя:");
        Console.WriteLine(customEmail);
    }
}
