using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System.Net.Mail;
using System.Text;

Console.WriteLine("Hello, World!");

var sender = new SmtpSender(() => new SmtpClient("localhost")
{
    EnableSsl = false,
    DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
    PickupDirectoryLocation = @"D:\Study\Shevchenko\4 course",
    
});

StringBuilder template = new();
template.AppendLine("Dear @Model.FirstName, ");
template.AppendLine("<p> Thanks for purchasing @Model.ProductName. We hope you enjoy it. </p>");
template.AppendLine("- The TimCo team");

Email.DefaultSender = sender;
Email.DefaultRenderer = new RazorRenderer();

var email = await Email
    .From("tim@timco.com")
    .To("test@test.com", "Sue")
    .Subject("Thanks!")
    .UsingTemplate(template.ToString(), new {FirstName = "Tim", ProductName = "Doritos"})
    //.Body("Thanks for buying our product")
    .SendAsync();
