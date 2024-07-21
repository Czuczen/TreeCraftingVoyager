using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace TreeCraftingVoyager.Server.Services.EmailSenderService;

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;

    public EmailSenderOptions Options { get; }


    public EmailSender(
        IOptions<EmailSenderOptions> optionsAccessor,
        ILogger<EmailSender> logger)
    {
        Options = optionsAccessor.Value;
        _logger = logger;
    }


    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        try
        {
            var client = new SmtpClient(Options.Server, Options.Port)
            {
                Credentials = new NetworkCredential(Options.EmailFrom, Options.EmailAddInfo),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(Options.EmailFrom, Options.EmailFromName),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while sending the email.", ex);
        }
    }
}
