
using EmailSender.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Asn1.Ocsp;


namespace EmailSender.Services
{   
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService ( IOptions<EmailSettings>setting )
        {
            _settings = setting.Value;
        }
        public async Task SendEmailAsync
        ( string toEmail, 
          string subject, 
          string bodyHtml
        , List<IFormFile> Attachments
        )
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            //email.Body = new TextPart("html") { Text = bodyHtml };
            var bodyBuilder = new BodyBuilder { HtmlBody = bodyHtml };
            if (Attachments != null)
            {
                foreach (var attachment in Attachments)
                {

                    if (attachment.Length > 0)
                    {
                        using var stream = new MemoryStream();
                        await attachment.CopyToAsync(stream);
                        bodyBuilder.Attachments.Add
                        (attachment.FileName, stream.ToArray(), ContentType.Parse(attachment.ContentType));

                    }
                }
            }
            email.Body = bodyBuilder.ToMessageBody();
            try
            {
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_settings.SmtpServer, _settings.Port, SecureSocketOptions.StartTls);//SecureSocketOptions Enum
                await smtp.AuthenticateAsync(_settings.Username, _settings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
